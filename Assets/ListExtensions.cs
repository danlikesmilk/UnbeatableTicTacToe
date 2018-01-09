using System;
using System.Collections.Generic;

public static class ListExtensions
{
	public static T GetMaxElement<T>(this List<T> list) where T : IComparable
	{
        T maxElement = list[0];
        for(int i = 1; i < list.Count; i++)
        {
            if(maxElement.CompareTo(list[i]) < 0)
            {
                maxElement = list[i];
            }
        }
        return maxElement;
	}

    public static T GetMinElement<T>(this List<T> list) where T : IComparable
    {
        T minElement = list[0];
        for (int i = 1; i < list.Count; i++)
        {
            if (minElement.CompareTo(list[i]) > 0)
            {
                minElement = list[i];
            }
        }
        return minElement;
    }

    public static int GetIndexOfMaxElement<T>(this List<T> list) where T : IComparable
    {
        int index = 0;
        T maxElement = list[0];
        for (int i = 1; i < list.Count; i++)
        {
            if (maxElement.CompareTo(list[i]) < 0)
            {
                maxElement = list[i];
                index = i;
            }
        }
        return index;
    }
}
