using System;

namespace decisiontree
{
	public class Value
	{

		private string item { get; set; }

		public Value (string value)
		{
			item = value;
		}

		public string AsString ()
		{
			return item;
		}

		public bool AsBool ()
		{
			if (item.ToLower ().Equals ("true")) {
				return true;
			} else if (item.ToLower ().Equals ("false")) {
				return false;
			} else {
				throw new SystemException("Could not parse as bool value: " + item);
			}
		}

		public override int GetHashCode ()
		{
			return item.GetHashCode();
		}

		public override bool Equals (object obj)
		{
			if (obj == null || GetType () != obj.GetType ()) {
				return false;
			}

			Value v = (Value)obj;
			return item.Equals(v.item);
		}

		public override string ToString ()
		{
			return string.Format ("[Value: {0}]", item);
		}
	}
	
}
