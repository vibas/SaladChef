using UnityEngine;
using UnityEditor;

public class SessionCleaner
{
    [MenuItem("SessionCleaner/Clear PlayerPrefs")]
    private static void NewMenuOption()
    {
        PlayerPrefs.DeleteAll();
    }
}
