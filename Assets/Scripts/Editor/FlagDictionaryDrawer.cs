using UnityEditor;

[CustomPropertyDrawer(typeof(FlagDictionary))] // Name of your class (same as above)
public class FlagDictionaryDrawer : DictionaryDrawer<string, int> { } // chose same types as your dictionary