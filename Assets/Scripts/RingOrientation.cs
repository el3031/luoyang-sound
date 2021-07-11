using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOrientation : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.Play();
        }
        
        Quaternion cameraRotation = Camera.main.transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, cameraRotation, 0.125f);
    }
}
