using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetalib
{
    [System.Serializable]
    public class KeyboardMap : SerializableDictionary<ZetaKey, KeyCode> { }
    [System.Serializable]
    public class MouseMap : SerializableDictionary<ZetaAxis, string> { }

    public class ZKeyboard : ZInstance<ZKeyboard>, InputInterface
    {
        //public KeyCode A = KeyCode.Mouse0;
        public KeyboardMap keymap = new KeyboardMap();
        public MouseMap mousemap = new MouseMap();

        protected override void Reset()
        {
            base.Reset();
            Init();
        }

        public void Init()
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
            mousemap.Add(ZetaAxis.MouseX, "Mouse X");
            mousemap.Add(ZetaAxis.MouseY, "Mouse Y");
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (keymap.Count == 0)
            {
                Init();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool CheckKeyDown(ZetaKey key)
        {
            if (keymap.ContainsKey(key))
            {
                return Input.GetKeyDown(keymap[key]);
            }
            print("Abstract " + key.ToString() + " key is not set.");
            return false;
        }

        public static bool ZGetKey(ZetaKey key)
        {
            return inst.CheckKey(key);
        }

        public static bool ZGetKeyDown(ZetaKey key)
        {
            return inst.CheckKeyDown(key);
        }

        public static float ZCheckAxis(ZetaAxis axis)
        {
            return inst.CheckAxis(axis);
        }

        public bool CheckKey(ZetaKey key)
        {
            if (keymap.ContainsKey(key))
            {
                return Input.GetKey(keymap[key]);
            }
            print("Abstract " + key.ToString() + " key is not set.");
            return false;
        }

        public float CheckAxis(ZetaAxis axis)
        {
            if (mousemap.ContainsKey(axis))
            {
                return Input.GetAxis(mousemap[axis]);
            }
            return 0;
        }
    }
}

