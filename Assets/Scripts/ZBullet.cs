using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Zetalib
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(ZObj))]
    public class ZBullet : MonoBehaviour
    {
        public Vector3 localSpeed = new Vector3(0, 0, 1);
        public Vector3 acceleration;
        public Vector3 gravity;
        public bool bounceOffSolid;
        public bool setAngle;
        public float autoDestroyTime;

        //private Collider col;
        //private Mesh mesh;
        //private float colRadius = 0;
        private ZObj zobj;
        private Vector3 lastPos;
        private Vector3 lastPos2;
        private Vector3 bulletWorldSpeed;
        private bool lastSetAngle;

        // Start is called before the first frame update
        void Start()
        {
            //mesh = GetComponent<MeshFilter>().mesh;
            //Vector3 scaledBounds = Vector3.Scale(mesh.bounds.size, transform.localScale);
            //colRadius = scaledBounds.magnitude / 2.0f;
            zobj = GetComponent<ZObj>();
            if (autoDestroyTime != 0)
            {
                Destroy(gameObject, autoDestroyTime);
            }
            if (localSpeed == Vector3.zero)
            {
                localSpeed = Vector3.forward;
            }
            bulletWorldSpeed = transform.TransformVector(localSpeed);
            lastSetAngle = setAngle;
        }

        // Update is called once per frame
        void Update()
        {
            // Detecte SetAngle change
            if (setAngle != lastSetAngle && setAngle == false)
            {
                bulletWorldSpeed = transform.TransformVector(localSpeed);
            }

            // Movement
            if (setAngle)
            {
                bulletWorldSpeed = transform.TransformVector(localSpeed);
            }
            else
            {

            }
            transform.Translate(bulletWorldSpeed * Time.deltaTime, Space.World);

            // Record Last Position
            lastPos2 = lastPos;
            lastPos = transform.position;
            lastSetAngle = setAngle;
        }

        public void OnOverlapStart(object param)
        {
            if (!enabled)
            {
                return;
            }

            GameObject target = param as GameObject;
            Vector3 dir = (target.transform.position - lastPos).normalized;
            RaycastHit[] hits = new RaycastHit[64];
            int hitNum = 0;
            //Vector3 bulletWorldSpeed = transform.TransformDirection(localSpeed);

            if (zobj.shape == ZObjShape.Sphere)
            {
                //hitNum = Physics.SphereCastNonAlloc(lastPos2, zobj.colRadius, bulletWorldSpeed.normalized, hits, bulletWorldSpeed.magnitude * Time.deltaTime + 0.1f);
                hitNum = Physics.SphereCastNonAlloc(lastPos2, zobj.colRadius, bulletWorldSpeed.normalized, hits, (transform.position - lastPos2).magnitude);
            }
            else if (zobj.shape == ZObjShape.Box)
            {
                hitNum = Physics.BoxCastNonAlloc(lastPos2, zobj.scaledBounds / 2.0f, bulletWorldSpeed.normalized, hits, transform.rotation, (transform.position - lastPos2).magnitude);
            }

            if (hitNum > 0)
            {
                for (int i = 0; i < hitNum; i++)
                {
                    if (hits[i].transform == target.transform) // Make sure we are dealing with the target GameObject
                    {
                        Solid solid = hits[i].transform.GetComponent<Solid>();
                        if (solid != null && solid.enabled && bounceOffSolid)
                        {
                            //print(hits[i].transform.name);
                            print("impact point: " + hits[i].point);
                            print("normal: " + hits[i].normal);
                            Vector3 targetWorldSpeed = Vector3.Reflect(bulletWorldSpeed, hits[i].normal);
                            if (setAngle)
                            {
                                transform.rotation = Quaternion.FromToRotation(bulletWorldSpeed, targetWorldSpeed) * transform.rotation;
                            }
                            bulletWorldSpeed = targetWorldSpeed;
                            localSpeed = transform.InverseTransformDirection(bulletWorldSpeed);
                            transform.position = lastPos; //Roll back to last position
                        }
                    }

                }
            }

        }

    }
}
