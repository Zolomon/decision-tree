using System;
using System.Collections.Generic;

namespace decisiontree
{
	public class Attribute
	{
		public int Index {get;set;}

		public string Name { get; set; }

		public List<Value> Values { get; set; }

		public Attribute (int id, string name, List<Value> types)
		{
			this.Index = id;
			this.Name = name;
			this.Values = types;
		}

		public override string ToString ()
		{
			return string.Format ("[Attribute: Id={2}, Name={0}, Type={1}]", Name, String.Join (",", Values), Index);
		}
	}
}

