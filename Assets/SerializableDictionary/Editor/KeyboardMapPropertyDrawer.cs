using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Zetalib;

public class KeyboardMapPropertyDrawer : SerializableDictionaryPropertyDrawer
{
    [CustomPropertyDrawer(typeof(KeyboardMap))]
    public class AnyKeyboardMapPropertyDrawer : SerializableDictionaryPropertyDrawer{ }
}
