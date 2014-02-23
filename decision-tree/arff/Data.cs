using System;
using System.Linq;
using System.Collections.Generic;

namespace decisiontree
{
	public class Data
	{
		public Value Target { get; set; }

		public Dictionary<string, Value> Values { get; set; }

		public Data (List<Attribute> attributes, string data)
		{
			Values = new Dictionary<string, Value> ();
			var values = data
				.Split (new char[] {','}, StringSplitOptions.None)
				.Select(x => x.Trim()).ToList();

			for (int i = 0; i < attributes.Count-1; i++) {
				Values.Add (attributes [i].Name, new Value (values [i]));
			}

			Target = new Value (values.Last());
		}

		public override string ToString ()
		{
			return string.Format ("[Data: Values={0}]", String.Join (",", 
			                      Values.Select (x => x.Value.AsString ()))
			);
		}

		public override int GetHashCode ()
		{
			return Values.GetHashCode ();
		}
	}

}

