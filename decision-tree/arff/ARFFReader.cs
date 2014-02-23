using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace decisiontree
{
	public class ARFFReader
	{
		private Regex relation = new Regex (@"^@(?i)RELATION(?-i)\s+(?<name>.*?)$");
		private Regex attribute = new Regex (@"^@(?i)ATTRIBUTE(?-i)\s+(?<name>.*?)\s+{(?<values>(.+))}$");
		private Regex data = new Regex (@"^@(?i)DATA(?-i)$");

		public ARFFReader ()
		{

		}

		public Arff Parse (StreamReader reader)
		{
			string line = null;
			Arff arff = new Arff ();

			bool parsingData = false;
			int attributeIdx = 0;

			while ((line = reader.ReadLine()) != null) {
				if (relation.IsMatch (line)) {
					Match match = relation.Match (line);

					arff.Relation = new Relation (match.Value);
					continue;
				}

				if (attribute.IsMatch (line)) {
					Match match = attribute.Match (line);

					string name = match.Groups ["name"].Value;
					List<Value> values = match
						.Groups ["values"]
						.Value
						.Split (new char[] {','}, StringSplitOptions.None)
						.ToList ()
						.Select (x => new Value(x.Trim ()))
						.ToList();

					var att = new Attribute (attributeIdx, name, values);

					arff.Attributes.Add (att);
					arff.Target = att; 

					attributeIdx++;

					continue;
				}

				if (data.IsMatch (line)) {
					parsingData = true;
					continue;
				}

				if (parsingData) {
					arff.Data.Add (new Data (arff.Attributes, line));
					continue;
				}
			}

			if (arff.Relation == null || arff.Attributes.Count == 0 || arff.Data.Count == 0) {
				throw new ParserException ();
			}

			// Remove the last attribute which is the Target attribute...
			arff.Attributes.RemoveAt (arff.Attributes.Count - 1);

			return arff;
		}

	}

}
