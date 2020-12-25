using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Zetalib
{
    public enum ZObjShape
    {
        Sphere,
        Box
    }

    [System.Serializable]
    public class ZEventGameObject : UnityEvent<GameObject>
    {

    }

    [System.Serializable]
    public class ZEvent : UnityEvent
    {

    }

    public class ZObj : MonoBehaviour
    {
        public ZObjShape shape;
        public ZEventGameObject OverlapStart;
        public ZEventGameObject IsOverlapping;
        public ZEventGameObject OverlapEnd;

        private Collider col;
        private Mesh mesh;
        [HideInInspector]
        public float colRadius = 0;
        [HideInInspector]
        public Vector3 scaledBounds;
        private List<GameObject> overlapObjs = new List<GameObject>();
        private List<GameObject> lastOverlapObjs = new List<GameObject>();

        void OnDrawGizmos()
        {
            UpdateSize();
            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;

            if (shape == ZObjShape.Sphere)
            {
                Gizmos.DrawWireSphere(Vector3.zero, Mathf.Max(mesh.bounds.size.x, mesh.bounds.size.y, mesh.bounds.size.z));
            }
            else if (shape == ZObjShape.Box)
            {
                Gizmos.DrawWireCube (Vector3.zero, mesh.bounds.size);
            }
        }

        private void Reset()
        {

        }

        void OnEnable()
        {
            ZSys.inst.RegisterZObj(this);
        }

        void OnDestroy()
        {
            ZSys.inst.UnregisterZObj(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            UpdateSize();
        }

        private void UpdateSize()
        {
            if (mesh == null)   
            {
                mesh = GetComponent<MeshFilter>().sharedMesh;
            }
            scaledBounds = Vector3.Scale(mesh.bounds.size, transform.localScale);
            colRadius = Mathf.Max(scaledBounds.x, scaledBounds.y, scaledBounds.z) / 2.0f;
        }

        // Update is called once per frame
        void Update()
        {
            overlapObjs.Clear();

            // Sphere Overlap Test
            RaycastHit[] hits = new RaycastHit[64];
            int hitNum = 0;
            if (shape == ZObjShape.Sphere)
            {
                hitNum = Physics.SphereCastNonAlloc(transform.position, colRadius, transform.forward, hits, 0);
            }
            else if (true)
            {
                hitNum = Physics.BoxCastNonAlloc(transform.position, scaledBounds / 2.0f, transform.forward, hits, transform.rotation, 0);
            }

            if (hitNum > 0)
            {
                for (int i = 0; i < hitNum; i++)
                {
                    if (hits[i].transform != transform)  // Exclude self.
                    {
                        //print(name + " is overlapping: " + hits[i].transform.name);
                        //SendMessage Overlaping here.
                        IsOverlapping.Invoke(hits[i].transform.gameObject);

                        overlapObjs.Add(hits[i].transform.gameObject);
                        if (!lastOverlapObjs.Contains(hits[i].transform.gameObject))
                        {
                            //SendMessage OverlapStart here.
                            print(name + " starts overlaping: " + hits[i].transform.name);
                            BroadcastMessage("OnOverlapStart", (object)hits[i].transform.gameObject, SendMessageOptions.DontRequireReceiver);
                            OverlapStart.Invoke(hits[i].transform.gameObject);
                        }
                    }
                }
            }

            foreach (GameObject lastOverlapObj in lastOverlapObjs)
            {
                if (!overlapObjs.Contains(lastOverlapObj))
                {
                    //SendMessage OverlapEnd here.
                    if (lastOverlapObj != null)
                    {
                        print(name + " ends overlaping: " + lastOverlapObj.name);
                        OverlapEnd.Invoke(lastOverlapObj.transform.gameObject);
                    }

                }
            }

            lastOverlapObjs.Clear();
            foreach (GameObject overlapObj in overlapObjs)
            {
                lastOverlapObjs.Add(overlapObj);
            }
        }

        public void DestroyZetaObj()
        {
            Destroy(gameObject);
        }

        public void KillZetaObj(GameObject target)
        {
            Destroy(target);
        }

        public static void Create(GameObject prefab, Vector3 position, float angleX, float angleY, float angleZ)
        {
            Instantiate(prefab, position, Quaternion.Euler(angleX, angleY, angleZ));
        }

        public static void Create(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            Instantiate(prefab, position, rotation);
        }

        public void SetPosition(float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
        }
    }
}
