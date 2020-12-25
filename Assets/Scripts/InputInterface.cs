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
        Vertical,
        MouseX,
        MouseY
    }

    public interface InputInterface  
    {
        //private static InputInterface<T> _inst;
        //public static InputInterface<T> inst
        //{
        //    get
        //    {
        //        if (_inst == null)
        //        {
        //            GameObject go = new GameObject(typeof(T).Name);
        //            _inst = go.AddComponent<T>();
        //        }
        //        return _inst;
        //    }
        //}
        //
        //protected void Awake()
        //{
        //    if (_inst != null)
        //    {
        //        Destroy(this);
        //    }
        //    _inst = this;
        //}

        bool CheckKeyDown(ZetaKey key);
        bool CheckKey(ZetaKey key);
        float CheckAxis(ZetaAxis axis);
        void Init();
    }
}
