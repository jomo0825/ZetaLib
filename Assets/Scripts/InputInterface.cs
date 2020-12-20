using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetalib
{
    public enum ZetaKey
    {
        A,
        B,
        C,
        D,
        Up,
        Down,
        Left,
        Right,
        Jup,
        Jdown,
        Jleft,
        Jright
    }

    public enum ZetaAxis
    {
        Horizontal,
        Vertical
    }

    public abstract class InputInterface<T> : MonoBehaviour  where T : InputInterface<T> 
    {
        private static InputInterface<T> _inst;
        public static InputInterface<T> inst
        {
            get
            {
                if (_inst == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    _inst = go.AddComponent<T>();
                }
                return _inst;
            }
        }

        protected void Awake()
        {
            if (_inst != null)
            {
                Destroy(this);
            }
            _inst = this;
        }

        public abstract bool CheckKeyDown(ZetaKey key);
        public abstract bool CheckKey(ZetaKey key);
        public abstract float CheckAxis(ZetaAxis axis);
    }
}
