using System;
using System.Collections.Generic;
using System.Linq;

namespace decisiontree
{
	public class Arff
	{
		public Relation Relation { get; set; }

		public List<Attribute> Attributes { get; set; }

		public List<Data> Data { get; set; }

		public Attribute Target { get; set; }

		public Arff ()
		{
			Attributes = new List<Attribute> ();
			Data = new List<Data> ();
		}

		public override string ToString ()
		{
			string[] attributes = (from attribute in Attributes
				select attribute.ToString ()).ToArray ();
			string[] datas = (from data in Data 
				select data.ToString ()).ToArray ();

			return string.Format ("[Arff: Relation={0}\nAttributes\n{1}\nTarget{3}\nData\n{2}\n]", Relation, 
			                      String.Join ("\n", attributes), 
			                      String.Join ("\n", datas),
			                      Target
			);
		}

		public Tuple<List<Data>, List<Data>> SplitRandomly() 
		{
			List<Data> clone = Data.ToList();
			clone.Shuffle();

			var middle = (clone.Count)/2;
			var remainder = clone.Count % 2;
			return new Tuple<List<Data>, List<Data>>(clone.Take(middle).ToList(), clone.Skip(middle).Take(middle+remainder).ToList());
		}
	}
}

