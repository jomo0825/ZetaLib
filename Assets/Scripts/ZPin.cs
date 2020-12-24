using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetalib
{
    public enum ZPinMode
    {
        POSITION_ANGLE,
        POSITION
    }

    public class ZPin : MonoBehaviour
    {
        [Range(0,100)]
        public float stiffness;

        private ZPinMode mode;
        private Transform targetTrans;
        private Vector3 offsetPos;
        private Quaternion offsetRot;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (targetTrans != null)
            {
                Vector3 targetPos = targetTrans.position;
                Quaternion targetRot = targetTrans.rotation;
                Vector3 tempVelocity = new Vector3();

                switch (mode)
                {
                    case ZPinMode.POSITION:
                        transform.position = Vector3.Slerp(transform.position, targetPos + targetTrans.TransformVector(offsetPos), 1 - Mathf.Exp(-stiffness * Time.deltaTime));
                        //transform.position = Vector3.SmoothDamp(transform.position, targetPos + targetTrans.TransformVector(offsetPos), ref tempVelocity, 0.1f);
                        break;
                    case ZPinMode.POSITION_ANGLE:
                        transform.position = Vector3.Slerp(transform.position, targetPos + targetTrans.TransformVector(offsetPos), 1 - Mathf.Exp(-stiffness * Time.deltaTime));
                        //transform.position =targetPos + targetTrans.TransformVector(offsetPos);
                        //transform.position = Vector3.SmoothDamp(transform.position, targetPos + targetTrans.TransformVector(offsetPos), ref tempVelocity, 0.1f);
                        transform.rotation = offsetRot * targetRot;
                        break;
                    default:
                        break;
                }
            }
        }

        public void PinTo(Transform trans, ZPinMode _mode)
        {
            mode = _mode;
            targetTrans = trans;
            offsetPos = targetTrans.InverseTransformVector( transform.position - targetTrans.position);
            offsetRot = Quaternion.FromToRotation(targetTrans.forward, transform.forward);
        }
    }
}

