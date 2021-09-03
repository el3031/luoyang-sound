using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour
{ 
    public Image menuImg;
    private bool menuFaded;
    private Image[] menuChildrenImg;
    private float[] menuChildrenAlpha;
    public Chapters chapters;

    public Image title;
    private Image[] titleChildrenImg;
    private float[] titleChildrenAlpha;
    private bool titleFaded;
    
    void Start()
    {
        menuFaded=false;
        menuChildrenImg = StaticImageFade.GetOriginalImages(menuImg);
        menuChildrenAlpha = StaticImageFade.GetOriginalAlphas(menuImg);
        StaticImageFade.QuickFade(menuChildrenImg);
        menuFaded = true;

        titleFaded = false;
        titleChildrenImg = StaticImageFade.GetOriginalImages(title);
        titleChildrenAlpha = StaticImageFade.GetOriginalAlphas(title);
    }
    public void OnClickMenu()
    {
        StartCoroutine(StaticImageFade.FadeImage(menuFaded=!menuFaded, 0.5f, menuChildrenAlpha, menuChildrenImg));
    }

    public void OnClickBuddhism()
    {
        Debug.Log("click buddhism");
        StopCoroutine(Army());
        StopCoroutine(Fire());
        StartCoroutine(Buddhism());
    }

    public IEnumerator Buddhism()
    {
        yield return StartCoroutine(FadeOutMainMenu());
        yield return StartCoroutine(chapters.BuddhismMain());
    }

    public void OnClickArmy()
    {
        StopCoroutine(Fire());
        StopCoroutine(Buddhism());
        StartCoroutine(Army());
    }

    public IEnumerator Army()
    {
        yield return StartCoroutine(FadeOutMainMenu());
        yield return StartCoroutine(chapters.ArmyMain());
    }

    public void OnClickSoundlib()
    {
        //
    }

    public void OnClickFire()
    {
        StopCoroutine(Buddhism());
        StopCoroutine(Army());
        StartCoroutine(Fire());
    }
    
    public IEnumerator Fire()
    {
        yield return StartCoroutine(FadeOutMainMenu());
        yield return StartCoroutine(chapters.FireMain());
    }

    public IEnumerator FadeOutMainMenu()
    {
        if (!menuFaded)
        {
            Debug.Log("fading menu");
            StartCoroutine(StaticImageFade.FadeImage(menuFaded=true, 1f, menuChildrenAlpha, menuChildrenImg));
        }
        if (!titleFaded)
        {
            Debug.Log("fading title");
            yield return StartCoroutine(StaticImageFade.FadeImage(titleFaded=true, 1f, titleChildrenAlpha, titleChildrenImg));
        }
        yield return new WaitForSeconds(1f);
    }

    public void OnClickExit()
    {
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_WEBGL)
            Application.OpenURL("https://gorfmcgorf.itch.io/");
        #endif
    }
}
