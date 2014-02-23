using System;

namespace decisiontree
{
	public class StringValue : Value
	{
		private string item { get; set; }

		public StringValue (string value) : base(value)
		{
			item = value;
		}

		public string Value ()
		{
			return item;
		}
	}
	
}
