using System.Collections;
using System.Collections.Generic;

public class Utility
{
    /// <summary>
    /// Checks if both list contents are equal
    /// Both list must be sorted and same number of elements
    /// </summary>
    /// <param name="list1"></param>
    /// <param name="list2"></param>
    /// <returns></returns>
    public static bool AreBothListEqual(List<string> list1, List<string> list2)
    {
        bool areBothListEqual = true;
        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i])
            {
                areBothListEqual = false;
                break;
            }
        }
        return areBothListEqual;
    }

    /// <summary>
    /// Before clearing a list , making a copy of that list for use.
    /// </summary>
    /// <param name="list1"></param>
    /// <returns></returns>
    public static List<string> GetACopyList(List<string> list1)
    {
        List<string> list2 = new List<string>();
        for (int i = 0; i < list1.Count; i++)
        {
            list2.Add(list1[i]);
        }
        return list2;
    }
}
