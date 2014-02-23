using System;
using System.Collections.Generic;
using System.Linq;

namespace decisiontree
{
	public class Leaf : Node
	{
		Value decision { get; set; }

		public Leaf (Value decision)
		{
			this.decision = decision;
		}

		public override Value Choose (Data example)
		{
			return this.decision;
		}

		public override int Size ()
		{
			return 1;
		}

		public override string Display (int depth)
		{
			return String.Format ("{0}", decision.AsString ());
		}

		public override bool IsLeaf ()
		{
			return true;
		}

	}

}

