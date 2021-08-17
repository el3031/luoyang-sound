using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapters : MonoBehaviour
{
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
    private WaitForSeconds twentySec;
    
    // Start is called before the first frame update
    void Start()
    {
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
        twentySec = new WaitForSeconds(20f);

        
        startChapter = PlayerPrefs.GetString("chapter");
        if (startChapter == "buddhism")
        {
            StartCoroutine(BuddhismMain());
        }
        else if (startChapter == "army")
        {
            StartCoroutine(ArmyMain());
        }
        else if (startChapter == "fire")
        {
            StartCoroutine(FireMain());
        }
    }

    public IEnumerator BuddhismMain()
    {
        //stuff here
        yield return StartCoroutine(DisplayText(buddhismAlpha, buddhismImage));
        
        StartCoroutine(ArmyMain());
    }

    public IEnumerator ArmyMain()
    {
        Debug.Log("army main");
        //stuff here
        yield return StartCoroutine(DisplayText(armyAlpha, armyImage));

        StartCoroutine(FireMain());
    }

    IEnumerator FireMain()
    {
        Debug.Log("fire main");
        yield return StartCoroutine(DisplayText(fireAlpha, fireImage));
        //stuff here
    }

    public IEnumerator DisplayText(float[] alphas, Image[] images)
    {
        yield return fiveSec;
        
        StartCoroutine(StaticImageFade.FadeImage(false, 1f, alphas, images));
        yield return twentySec;
        StartCoroutine(StaticImageFade.FadeImage(true, 1f, alphas, images));

        yield return fiveSec;
        yield return fiveSec;
    }
}
