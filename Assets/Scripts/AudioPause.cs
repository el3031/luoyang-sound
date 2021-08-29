using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPause : MonoBehaviour
{
    public bool paused;
    public bool started;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && started)
        {
            if (paused)
            {
                audio.Play();
            }
            else
            {
                audio.Pause();
            }
            paused = !paused;
        }
    }
}
