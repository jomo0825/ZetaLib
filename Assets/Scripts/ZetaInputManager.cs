using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetalib
{
    public enum ZetaInputType
    {
        Keyboard,
        KeyboardMouse,
        JoyStick
    }

    public class ZetaInputManager : MonoBehaviour
    {
        public ZetaEvent OnFire1;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            //if (Input.GetButtonDown("Fire1"))
            //{
            //    OnFire1.Invoke();
            //}
        }
    }
}
