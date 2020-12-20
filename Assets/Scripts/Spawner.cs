using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetalib
{
    public class Spawner : MonoBehaviour
    {
        public GameObject spawnObj;

        public void SpawnHere() { 
            Instantiate(spawnObj, transform.position, transform.rotation * spawnObj.transform.rotation);
        }
    }
}
