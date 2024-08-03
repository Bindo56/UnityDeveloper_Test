using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public float zoomSpeed = 2f;

    void Update()
    {
       
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
          
            if (Input.GetMouseButton(0))
            {
                float h = Input.GetAxis("Mouse X");
                float v = Input.GetAxis("Mouse Y");

                transform.Translate(-h * moveSpeed, -v * moveSpeed, 0);
            }

           
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            transform.Translate(0, 0, scroll * zoomSpeed, Space.Self);
        }
    }
}
