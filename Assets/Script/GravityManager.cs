using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{

    [SerializeField] HoloGravityManipulations gravityManipulations;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (gravityManipulations.confirmDown)
        {
            Physics.gravity = new Vector3(0, -9.81f, 0);
            Debug.Log("GravityNormal" + Physics.gravity);
        }

      
        if (gravityManipulations.confirmReverse)
        {
            Physics.gravity = new Vector3(0, 9.81f, 0);
            Debug.Log("GravityReverse" + Physics.gravity);
        }

    
        if (gravityManipulations.confirmLeft)
        {
            Physics.gravity = new Vector3(-9.81f, 0, 0);
            Debug.Log("GravityOnLeftSide" + Physics.gravity);
        }

      
        if (gravityManipulations.confirmRight)
        {
            Physics.gravity = new Vector3(9.81f, 0, 0);
            Debug.Log("GravityOnRightSide" + Physics.gravity);
        }
    }
}
