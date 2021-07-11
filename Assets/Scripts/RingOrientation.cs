using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOrientation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion cameraRotation = Camera.main.transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, cameraRotation, Time.deltaTime);
    }
}
