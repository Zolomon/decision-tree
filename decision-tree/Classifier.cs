using System;
using System.IO;

namespace decisiontree
{
	class Classifier
	{
		Arff arff { get; set; }
		DecisionBuilder builder { get; set; }
		bool prune { get; set; }

		public Classifier (Arff arff, bool prune=false)
		{
			this.arff = arff;
			this.builder = new DecisionBuilder (this.arff);
			this.prune = prune;
		}

		public void DrawTree ()
		{
			var tree = builder.Build (arff.Data, arff.Attributes, false);
			Console.WriteLine(tree.Display(0));
		}

		public static void Main (string[] args)
		{
//			if (args.Length > 2) {
//				Environment.Exit (1);
//			}
//
//			bool prune = false;
//			string filename = args [0];
//
//			if (args.Length == 2) {
//				prune = args [1].Equals ("prune");
//
//				if (!prune) {
//					Console.WriteLine ("Pruning is disabled, could not parse \"prune\".");
//				}
//			} 

			string filename = "data/example.arff";

			ARFFReader reader = new ARFFReader();
			Arff arff = reader.Parse(new StreamReader(filename));

			Classifier c = new Classifier(arff, true);
			c.DrawTree();
		}
	}
}
