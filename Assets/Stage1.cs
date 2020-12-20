using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zetalib;

public class Stage1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ZKeyboard.inst.CheckKeyDown(ZetaKey.A))
        {
            print("A is pressed.");
        }
        
    }
}
