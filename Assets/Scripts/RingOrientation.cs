using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOrientation : MonoBehaviour
{
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
            RaycastHit hit;  
            if (Physics.Raycast(ray, out hit)) {  
                Debug.Log(hit.transform.name);
                //Select stage    
                if (hit.transform.name == this.gameObject.name) {
                    audioSource.Play();
                }
            }
        }
        */
        
        Quaternion cameraRotation = Camera.main.transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, cameraRotation, 0.5f);
    }
}
