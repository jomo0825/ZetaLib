using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetalib
{
    public class ZInstance<T> : MonoBehaviour where T : ZInstance<T>
    {
        private static T _inst;

        protected virtual void Reset()
        {
            CheckAndAdd();
        }

        protected virtual void OnEnable()
        {
            CheckAndAdd();
        }

        private void CheckAndAdd()
        {
            if (_inst == null)
            {
                _inst = (T)this;
            }
            else if (_inst != this)
            {
                DestroyImmediate(this);
            }
        }

        public static T inst
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
    }
}
