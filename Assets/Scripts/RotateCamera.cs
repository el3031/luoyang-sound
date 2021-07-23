using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 initialMousePos;
    private Vector3 finalMousePos;
    [SerializeField] private float rotateSpeed;
    private bool startRotate;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialMousePos = Input.mousePosition;
            Debug.Log("mouse down");
        }
        if (Input.GetMouseButtonUp(0))
        {
            finalMousePos = Input.mousePosition;
            Debug.Log("mouse up");
            StartCoroutine(Rotate());
        }

    }

    IEnumerator Rotate()
    {
        float xMove = finalMousePos.x - initialMousePos.x;
        float time = Mathf.Clamp(Mathf.Abs(xMove), 0f, 1f);
        float timer = 0f;
        while (timer < time)
        {
            yield return null;
            timer += Time.deltaTime;
            float ratio = (xMove < 0) ? -1 : 1;
            transform.Rotate(0, time * ratio, 0);
        }
    }
}
