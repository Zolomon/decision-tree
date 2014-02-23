using System;

namespace decisiontree
{
	public class Relation
	{
		public string Name { get; set; }

		public Relation (string name)
		{
			this.Name = name;
		}

		public override string ToString ()
		{
			return string.Format ("[Relation: Name={0}]", Name);
		}
	}

}

