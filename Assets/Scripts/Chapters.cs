﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapters : MonoBehaviour
{
    /*
    private string startChapter;
    public Image buddhism;
    public Image army;
    public Image fire;
    private float[] buddhismAlpha;
    private Image[] buddhismImage;
    private float[] armyAlpha;
    private Image[] armyImage;
    private float[] fireAlpha;
    private Image[] fireImage;

    private WaitForSeconds fiveSec;
*/
    public GameObject buddhismButton;
    public GameObject armyButton;
    public GameObject fireButton;

    public AudioSource buddhismAudio;
    public AudioSource armyAudio;
    public AudioSource fireAudio;
    //public GameObject armyButton;
    //public GameObject fireButton;
    
    // Start is called before the first frame update
    void Start()
    {
        /*
        buddhismImage = StaticImageFade.GetOriginalImages(buddhism);
        buddhismAlpha = StaticImageFade.GetOriginalAlphas(buddhism);

        armyImage = StaticImageFade.GetOriginalImages(army);
        armyAlpha = StaticImageFade.GetOriginalAlphas(army);

        fireImage = StaticImageFade.GetOriginalImages(fire);
        fireAlpha = StaticImageFade.GetOriginalAlphas(fire);
        
        StaticImageFade.QuickFade(buddhismImage);
        StaticImageFade.QuickFade(fireImage);
        StaticImageFade.QuickFade(armyImage);

        fiveSec = new WaitForSeconds(5f);
*/
       /*
        else if (startChapter == "army")
        {
            StartCoroutine(ArmyMain());
        }
        else if (startChapter == "fire")
        {
            StartCoroutine(FireMain());
        }
        */
    }

    public IEnumerator BuddhismMain()
    {
        buddhismButton.active = true;
        while (!buddhismAudio.GetComponent<AudioPause>().started ||
               buddhismAudio.GetComponent<AudioPause>().paused ||
               buddhismAudio.isPlaying)
        {
            yield return null;
        }
        StartCoroutine(ArmyMain());
    }

    /*
    public IEnumerator BuddhismMainSounds()
    {
        
        
        yield return StartCoroutine(ArmyMain());
    }
*/

    public IEnumerator ArmyMain()
    {
        armyButton.active = true;
        while (!armyAudio.GetComponent<AudioPause>().started ||
               armyAudio.GetComponent<AudioPause>().paused ||
               armyAudio.isPlaying)
        {
            yield return null;
        }
        StartCoroutine(FireMain());
        
        //stuff here
        //yield return StartCoroutine(DisplayText(armyAlpha, armyImage));

        //StartCoroutine(FireMain());
    }

    public IEnumerator FireMain()
    {
        fireButton.active = true;
        while (!fireAudio.GetComponent<AudioPause>().started ||
               fireAudio.GetComponent<AudioPause>().paused ||
               fireAudio.isPlaying)
        {
            yield return null;
        }
    }

}
