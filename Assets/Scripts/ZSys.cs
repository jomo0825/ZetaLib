using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetalib
{
    public class ZSys : ZInstance<ZSys>
    {
        [SerializeField]
        private List<ZObj> zobjs = new List<ZObj>();

        public void RegisterZObj(ZObj target) {
            if (!zobjs.Contains(target))
            {
                zobjs.Add(target);
            }
        }

        public void UnregisterZObj(ZObj target) {
            if (zobjs.Contains(target))
            {
                zobjs.Remove(target);
            }
        }

        protected override void Reset()
        {
            base.Reset();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }
    }

}
