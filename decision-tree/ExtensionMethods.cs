using System;
using System.Collections.Generic;
using System.Linq;

namespace decisiontree
{
	public static class ExtensionMethods
	{
		public static bool IsEmpty<T> (this List<T> list)
		{
			return list.Count == 0;
		}

		public static IList<T> Clone<T> (this IList<T> listToClone) where T: ICloneable
		{
			return listToClone.Select (item => (T)item.Clone ()).ToList ();
		}

		// http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp
		public static void Shuffle<T> (this IList<T> list)
		{  
			Random rng = new Random ();  
			int n = list.Count;  
			while (n > 1) {  
				n--;  
				int k = rng.Next (n + 1);  
				T value = list [k];  
				list [k] = list [n];  
				list [n] = value;  
			}  
		}

		public static IEnumerable<int> To (this int from, int to)
		{
			if (from < to) {
				while (from <= to) {
					yield return from++;
				}
			} else {
				while (from >= to) {
					yield return from--;
				}
			}
		}

		public static IEnumerable<T> Step<T> (this IEnumerable<T> source, int step)
		{
			if (step == 0) {
				throw new ArgumentOutOfRangeException ("step", "Param cannot be zero.");
			}

			return source.Where ((x, i) => (i % step) == 0);
		}
	}
}

