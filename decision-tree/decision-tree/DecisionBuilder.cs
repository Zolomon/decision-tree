using System;
using System.Collections.Generic;
using System.Linq;

namespace decisiontree
{
	public class DecisionBuilder
	{
		private Arff arff { get; set; }

		public DecisionBuilder (Arff arff)
		{
			this.arff=arff;
		}

		public Node Build (List<Data> examples, List<Attribute> attributes, bool prune=false)
		{
			return this.Learn (examples, arff.Attributes, examples, prune);
		}

		private Node Learn (List<Data> examples, List<Attribute> attributes, List<Data> parent_examples, bool prune=false)
		{
			if (examples.Count == 0) {
				return new Leaf (this.Plurality (parent_examples));
			} else if (this.SameClassification (examples)) {
				return new Leaf (examples [0].Target);
			} else if (attributes.Count == 0) {
				return new Leaf (this.Plurality (examples));
			} else {
				var best = this.BestAttribute(examples, attributes);
				var filtered_attributes = attributes.ToList ();
				filtered_attributes.RemoveAll(x => x.Name == best.Name);
				var tree = new Node(best);
				var partitions = this.Partition(examples, best);
				var values = best.Values;

				foreach (var kvp in partitions) {
					tree.AddChild(kvp.Key, this.Learn(kvp.Value, filtered_attributes, examples, prune));
					if (values.Contains(kvp.Key)) {
						values.Remove(kvp.Key);
					}
				}

				foreach (var value in values) {
					tree.AddChild(value, new Leaf(this.Plurality(examples)));
				}

				return tree;
			}
		}

		private Dictionary<Value, List<Data>> Partition(List<Data> examples, Attribute best) 
		{
			var partitions = new Dictionary<Value, List<Data>>();

			foreach (var example in examples) {
				var key = example.Values[best.Name];
				if (partitions.ContainsKey(key)) {
					partitions[key].Add (example);
				} else {
					partitions.Add (key, new List<Data>(){example});
				}
			}

			return partitions;
		}

		private bool SameClassification(List<Data> examples) 
		{
			var byBool = examples.Select(x => x.Target.AsBool()).ToList();
			var distinct = byBool.Distinct().ToList();
			var skipFirst = distinct.Skip(1).ToList();
			var any = skipFirst.Any ();

			return !any;

			//return examples.Select(x => x.Target.AsBool()).Distinct().Skip(1).Any ();
		}

		private Value Plurality(List<Data> examples) 
		{
			var high = examples[0].Target;
			var counts = new Dictionary<Value, int>() { {high, 0}};
			foreach (var item in examples) {
				var label = item.Target;
				if (counts.ContainsKey(label)) {
					counts[label]++;
					if (counts[label] > counts[high]) {
						high = label;
					}
				} else {
					counts.Add(label, 0);
				}
			}
			return high;
		}

		private Attribute BestAttribute(List<Data> examples, List<Attribute> attributes) 
		{
			var best = attributes[0];
			var best_change = this.EntropyDifference(examples, best);
			foreach (var attribute in attributes) {
				var change = this.EntropyDifference(examples, attribute);
				if (change > best_change) {
					best = attribute;
					best_change = change;
				}
			}
			return best;
		}

		private double EntropyDifference(List<Data> examples, Attribute attribute) 
		{
			return this.Entropy (examples) - this.SplitEntropy(examples, attribute);
		}

		private double Entropy(List<Data> examples) 
		{
			var label_counts = new Dictionary<Value, int>();
			foreach (var example in examples) {
				if (label_counts.ContainsKey(example.Target)) {
					label_counts[example.Target]++;
				} else {
					label_counts.Add(example.Target, 1);
				}
			}
			return this.Entropy (label_counts, examples.Count);
		}

		private double SplitEntropy (List<Data> examples, Attribute attribute)
		{
			var partitions = this.Partition(examples, attribute);
			double entropy = 0;
			foreach (var kvp in partitions) {
				var splitentropy = this.Entropy (kvp.Value);
				entropy += (kvp.Value.Count / (double)(examples.Count)) * splitentropy;
			}
			return entropy;
		}

		private double Entropy(Dictionary<Value, int> label_counts, int total) 
		{
			double entropy = 0;
			double part = 0;
			foreach (var kvp in label_counts) {
				var count = kvp.Value;
				double rate = count / (double)total;
				if (rate != 0) {
					part = rate * Math.Log(rate, 2);
				}
				entropy += part;
			}
			// TODO: return entropy*-1? 
			return part*-1;
		}
	}
}

