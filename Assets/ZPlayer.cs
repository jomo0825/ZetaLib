using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetalib
{
    [RequireComponent(typeof(CharacterController))]
    public class ZPlayer : MonoBehaviour
    {
        public float maxSpeed = 8.0f;
        public float acceleration = 40.0f;
        public float deceleration = 40.0f;
        public Vector3 gravity = new Vector3(0, -30, 0);
        public float jumpSpeed = 13.0f;
        [Range(0,1.0f)]
        public float turnSpeed = 0.0f;
        public Spawner gun;
        public bool cursorLock = false;
        public Vector3 speed;
        public bool ignoreInput = false;
        public bool localMode = false;
        public GameObject globalAxisReferenceObj;
        private GameObject globalAlignObj;

        private CharacterController cc;
        //private GameObject rig;
        private Vector3 dir;
        private Vector3 thrust;

        // Start is called before the first frame update
        void Start()
        {
            cc = GetComponent<CharacterController>();
            globalAlignObj = new GameObject();
            //rig = transform.Find("rig").gameObject;
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

            if (globalAxisReferenceObj != null)
            {
                globalAlignObj.transform.forward = Vector3.ProjectOnPlane(globalAxisReferenceObj.transform.forward, Vector3.up);
            }
            else
            {
                globalAlignObj.transform.forward = Vector3.forward;
            }

            //if (ZKeyboard.inst.CheckKey(ZetaKey.Right))
            //{
            //    dir += transform.right;
            //}
            //else if (ZKeyboard.inst.CheckKey(ZetaKey.Left))
            //{
            //    dir += -transform.right;
            //}
            //
            //if (ZKeyboard.inst.CheckKey(ZetaKey.Up))
            //{
            //    dir += transform.forward;
            //}
            //else if (ZKeyboard.inst.CheckKey(ZetaKey.Down))
            //{
            //    dir += -transform.forward;
            //}

            if (ignoreInput)
            {
                dir = Vector3.zero;
                thrust = Vector3.zero;
            }

            dir = Vector3.ProjectOnPlane(dir, Vector3.up);

            if (dir.magnitude > 0)
            {
                speed += acceleration * dir.normalized * Time.deltaTime;
            }
            else
            {
                if (speed.magnitude > 0.1f)
                {
                    Vector3 projectedSpeed = new Vector3(speed.x, 0, speed.z);
                    Vector3 tempSpeed = projectedSpeed;

                    projectedSpeed -= deceleration * projectedSpeed.normalized * Time.deltaTime;
                    if (Vector3.Angle(projectedSpeed, tempSpeed) > 179f)
                    {
                        speed = new Vector3(0, speed.y, 0);
                    }
                    else
                    {
                        speed = new Vector3(projectedSpeed.x, speed.y, projectedSpeed.z);
                    }
                }
                else
                {
                    speed = new Vector3(0, speed.y, 0);
                }
            }
            Vector2 planar = new Vector2(speed.x, speed.z);
            planar = Mathf.Min(planar.magnitude, maxSpeed) * planar.normalized;
            speed = new Vector3(planar.x, speed.y, planar.y);

            speed += thrust;
            speed += gravity * Time.deltaTime;

            //cc.SimpleMove(speed);
            //cc.Move(speed * Time.deltaTime); // speed as local speed.
            if (dir.magnitude != 0)
            {
                if (localMode && planar.magnitude > 0.5f)
                {
                    transform.forward = Vector3.Slerp(transform.forward, transform.TransformVector(new Vector3(speed.x, 0, Mathf.Abs(speed.z))), Mathf.Clamp01(turnSpeed)*0.1f); // global forward set to local speed.
                }
                else
                {
                    transform.forward = Vector3.Slerp(transform.forward, globalAlignObj.transform.TransformVector( new Vector3(speed.x, 0, speed.z)), Mathf.Clamp01(turnSpeed)); // global forward set to global speed.
                }
            }

            if (localMode)
            {
                cc.Move(transform.TransformVector(Vector3.Scale(speed, new Vector3(0, 1, 1)) * Time.deltaTime)); // speed as local speed
            }
            else
            {
                cc.Move(globalAlignObj.transform.TransformVector( speed) * Time.deltaTime); // speed as global speed.
            }

            if (cc.isGrounded)
            {
                speed.y = 0;
            }


            dir = Vector3.zero;
            thrust = Vector3.zero;

            //transform.Rotate(transform.up, ZKeyboard.inst.CheckAxis(ZetaAxis.Horizontal) * 100 * Time.deltaTime);
            //rig.transform.Rotate(Vector3.right, ZKeyboard.inst.CheckAxis(ZetaAxis.Vertical) * -100 * Time.deltaTime, Space.Self);

            if (ZKeyboard.inst.CheckKeyDown(ZetaKey.A))
            {
                if (gun != null)
                {
                    gun.SpawnHere();
                }
            }
        }

        public void SimulateLeft()
        {
            dir += -Vector3.right;
        }

        public void SimulateRight()
        {
            dir += Vector3.right;
        }

        public void SimulateForward()
        {
            dir += Vector3.forward;
        }

        public void SimulateBackward()
        {
            dir += -Vector3.forward;
        }

        public void SimulateJump()
        {
            if (cc.isGrounded)
            {
                thrust = Vector3.up * jumpSpeed;
            }
        }

        //public void MoveTo(Vector3 target)
        //{
        //    Vector3 offset = target - transform.position;
        //    if (offset.magnitude > 1f)
        //    {
        //        dir = transform.InverseTransformVector( offset);
        //        transform.forward = Vector3.ProjectOnPlane(offset.normalized, Vector3.up);
        //    }
        //}

        public void Stop()
        {
            speed = Vector3.zero;
        }
    }
}

