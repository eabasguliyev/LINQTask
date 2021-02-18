using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class ListExtensions
    {
        public static T GetMostOccurrenceData<T>(this List<T> list)
        {
            if (list == null)
                throw new NullReferenceException("List is null!");
            if (list.Count == 0)
                throw new ArgumentException("There is no data!");

            var uniqueList = new List<T>();

            T returnAble = default;
            var maxCount = 0;

            for (int i = 0; i < list.Count; i++)
            {
                if (uniqueList.Contains(list[i]))
                    continue;

                uniqueList.Add(list[i]);

                var count = list.Count(d => d.Equals(list[i]));
				
                if (count >= maxCount)
                {
                    returnAble = list[i];
                    maxCount = count;
                }
            }
            return returnAble;
        }
    }
}