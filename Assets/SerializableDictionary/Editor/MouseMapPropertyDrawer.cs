using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Zetalib;

public class MouseMapPropertyDrawer : SerializableDictionaryPropertyDrawer
{
    [CustomPropertyDrawer(typeof(MouseMap))]
    public class AnyMouseMapPropertyDrawer : SerializableDictionaryPropertyDrawer{ }
}
