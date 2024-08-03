using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Cinemachine;
using Unity.VisualScripting;

public class HoloGravityManipulations : MonoBehaviour
{
    public Rigidbody characterRigidbody;    
    public GameObject hologram;
    public float gravityStrength = 9.81f;

    public Vector3 gravityDirection = Vector3.down;
    public Vector3 chrachterGravity = Vector3.down;
    public Vector3 holo = Vector3.down;



    public bool down;
    public bool up;
    public bool left;
    public bool right;

    public bool confirmDown;
    public bool confirmReverse;
    public bool confirmRight;
    public bool confirmLeft;
    private void Update()
    {
       holo = hologram.transform.position;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            gravityDirection = new Vector3(0, 0, 0);
            chrachterGravity = new Vector3(0, 0, 0);
            down = true;
            up = false;
            right = false;
            left = false;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            gravityDirection = new Vector3(180,0,0);
            chrachterGravity = new Vector3(0, 180, 0);
            up = true;
            down = false;
            left = false;
            right = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            gravityDirection = new Vector3(0, 0, 90);
            chrachterGravity = new Vector3(-90, 0, 0);
            left = true;
            right = false;
            up = false;
            down = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            gravityDirection = new Vector3(90, 0, 0);
            chrachterGravity = new Vector3(0, 0, -90);
            right = true;
            up = false;
            down = false;
            left = false;
        }

      
        if (hologram != null)
        {
            hologram.transform.rotation = Quaternion.Euler(gravityDirection);
        }


        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (down)
            {
                confirmDown = true;
                confirmReverse = false;
                confirmRight = false;
                confirmLeft = false;
                
            }
            else if (up)
            {
                confirmDown = false;
                confirmReverse = true;
                confirmRight = false;
                confirmLeft = false;
            }
            else if (right)
            {
                confirmDown = false;
                confirmReverse = false;
                confirmRight = true;
                confirmLeft = false;
            }
            else if (left)
            {
                confirmDown = false;
                confirmReverse = false;
                confirmRight = false;
                confirmLeft = true;
            }
            else
            {
                confirmDown = false;
                confirmReverse = false;
                confirmRight = false;
                confirmLeft = false;
            }

           // Physics.gravity = chrachterGravity * gravityStrength;
            //Physics.gravity = chrachterGravity;


            Quaternion targetRotation = Quaternion.Euler(gravityDirection);
            characterRigidbody.transform.rotation = targetRotation;
        }
        
    }

    
  

}
