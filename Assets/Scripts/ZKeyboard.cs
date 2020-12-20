using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetalib
{
    [System.Serializable]
    public class KeyboardMap : SerializableDictionary<ZetaKey, KeyCode> { }
    [System.Serializable]
    public class MouseMap : SerializableDictionary<ZetaAxis, string> { }

    public class ZKeyboard : InputInterface<ZKeyboard>
    {
        //public KeyCode A = KeyCode.Mouse0;
        public KeyboardMap keymap = new KeyboardMap();
        public MouseMap mousemap = new MouseMap();

        void Reset()
        {
            keymap.Clear();
            keymap.Add(ZetaKey.A, KeyCode.J);
            keymap.Add(ZetaKey.B, KeyCode.K);
            keymap.Add(ZetaKey.Left, KeyCode.A);
            keymap.Add(ZetaKey.Right, KeyCode.D);
            keymap.Add(ZetaKey.Up, KeyCode.W);
            keymap.Add(ZetaKey.Down, KeyCode.S);
            keymap.Add(ZetaKey.Jup, KeyCode.UpArrow);
            keymap.Add(ZetaKey.Jdown, KeyCode.DownArrow);
            keymap.Add(ZetaKey.Jleft, KeyCode.LeftArrow);
            keymap.Add(ZetaKey.Jright, KeyCode.RightArrow);

            mousemap.Add(ZetaAxis.Horizontal, "Horizontal");
            mousemap.Add(ZetaAxis.Vertical, "Vertical");

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override bool CheckKeyDown(ZetaKey key)
        {
            if (keymap.ContainsKey(key))
            {
                return Input.GetKeyDown(keymap[key]);
            }
            print("Abstract " + key.ToString() + " key is not set.");
            return false;
        }

        public override bool CheckKey(ZetaKey key)
        {
            if (keymap.ContainsKey(key))
            {
                return Input.GetKey(keymap[key]);
            }
            print("Abstract " + key.ToString() + " key is not set.");
            return false;
        }

        public override float CheckAxis(ZetaAxis axis)
        {
            if (mousemap.ContainsKey(axis))
            {
                return Input.GetAxis(mousemap[axis]);
            }
            return 0;

        }
    }
}

