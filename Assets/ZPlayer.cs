using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetalib
{
    [RequireComponent(typeof(CharacterController))]
    public class ZPlayer : MonoBehaviour
    {
        public float speed = 5.0f;
        public Spawner gun;
        public bool cursorLock = false;

        private CharacterController cc;
        private GameObject rig;

        // Start is called before the first frame update
        void Start()
        {
            cc = GetComponent<CharacterController>();
            rig = transform.Find("rig").gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            if (cursorLock)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            Vector3 dir = Vector3.zero;
            if (ZKeyboard.inst.CheckKey(ZetaKey.Right))
            {
                dir += transform.right;
            }
            else if (ZKeyboard.inst.CheckKey(ZetaKey.Left))
            {
                dir += -transform.right;
            }

            if (ZKeyboard.inst.CheckKey(ZetaKey.Up))
            {
                dir += transform.forward;
            }
            else if (ZKeyboard.inst.CheckKey(ZetaKey.Down))
            {
                dir += -transform.forward;
            }

            dir = dir.normalized * speed;
            
            cc.SimpleMove(dir);

            //if (ZKeyboard.inst.CheckKey(ZetaKey.Jright))
            //{
            //    transform.Rotate(transform.up, 100 * Time.deltaTime);
            //}
            //else if (ZKeyboard.inst.CheckKey(ZetaKey.Jleft))
            //{
            //    transform.Rotate(transform.up, -100 * Time.deltaTime);
            //}
            transform.Rotate(transform.up, ZKeyboard.inst.CheckAxis(ZetaAxis.Horizontal) * 100 * Time.deltaTime);
            rig.transform.Rotate(Vector3.right, ZKeyboard.inst.CheckAxis(ZetaAxis.Vertical) * -100 * Time.deltaTime, Space.Self);

            if (ZKeyboard.inst.CheckKeyDown(ZetaKey.A))
            {
                if (gun!=null)
                {
                    gun.SpawnHere();
                }
            }
        }
    }
}

