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
			var tree = builder.BuildTree (arff.Data, arff.Attributes, true);
			Console.WriteLine(tree.Display(0));
		}

		public static void Main (string[] args)
		{
			if (args.Length > 2 || args.Length == 0) {
				Console.WriteLine ("Bad usage. Use:\t./decision-tree.exe <arff file> \t[prune]");
				Console.WriteLine ("Example: \t./decision-tree.exe data/example.arff \tprune");
				Environment.Exit (1);
			}

			bool prune = false;
			string filename = args [0];

			if (args.Length == 2) {
				prune = args [1].Equals ("prune");

				if (!prune) {
					Console.WriteLine ("Pruning is disabled, could not parse \"prune\".");
				}
			} 

			if (!File.Exists (filename)) {
				Console.WriteLine("File \"{0}\" does not exist.", filename);
				Environment.Exit(1);
			}

			Console.WriteLine ("File: \t\t{0}\nPruning: \t{1}", filename, prune);

			ARFFReader reader = new ARFFReader();
			Arff arff = reader.Parse(new StreamReader(filename));

			Classifier c = new Classifier(arff, prune);
			c.DrawTree();
		}
	}
}
