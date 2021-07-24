using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform max;
    [SerializeField] private Transform min;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        Vector3 yMove = Vector3.up * Input.mouseScrollDelta.y;
        
        /*
        float targetX = Mathf.Clamp(xMove + transform.position.x, xMin.position.x, xMax.position.x);
        float targetZ = Mathf.Clamp(zMove + transform.position.z, zMin.position.z, zMax.position.z);
        */
        
        Vector3 camX = transform.right;
        Vector3 camZ = transform.forward;
        camX = camX.normalized;
        camZ = camZ.normalized;

        Vector3 target = transform.position + (camX * xMove + camZ * zMove + yMove);
        Vector3 actual = Vector3.zero;
        if (inBounds(target.x, 'x'))
        {
            actual += camX * xMove;
        }
        if (inBounds(target.y, 'y'))
        {
            actual += yMove;
        }
        if (inBounds(target.z, 'z'))
        {
            actual += camZ * zMove;
        }
        transform.position += (actual) * Time.deltaTime * 100f;

    }

    private bool inBounds(float current, char axis)
    {
        float minBound;
        float maxBound;
        
        if (axis == 'x')
        {
            minBound = min.position.x;
            maxBound = max.position.x;
        }
        else if (axis == 'y')
        {
            minBound = min.position.y;
            maxBound = max.position.y;
        }
        else
        {
            minBound = min.position.z;
            maxBound = max.position.z;
        }

        return current >= minBound && current <= maxBound;
    }
}
