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

    public Image langSelect;
    private Image[] langSelectChildrenImg;
    private float[] langSelectChildrenAlpha;
    private bool langFaded;

    public Image exitPopup;
    
    void Start()
    {
        menuFaded=false;
        menuChildrenImg = StaticImageFade.GetOriginalImages(menuImg);
        menuChildrenAlpha = StaticImageFade.GetOriginalAlphas(menuImg);
        StaticImageFade.QuickFade(menuChildrenImg);
        menuImg.enabled = false;
        menuFaded = true;

        titleFaded = false;
        titleChildrenImg = StaticImageFade.GetOriginalImages(title);
        titleChildrenAlpha = StaticImageFade.GetOriginalAlphas(title);

        langSelectChildrenAlpha = StaticImageFade.GetOriginalAlphas(langSelect);
        langSelectChildrenImg = StaticImageFade.GetOriginalImages(langSelect);
        langFaded = false;
    }
    public void OnClickMenu()
    {
        StartCoroutine(OnClickMenuHelper());
    }

    public IEnumerator OnClickMenuHelper()
    {
        menuImg.enabled = true;
        yield return StartCoroutine(StaticImageFade.FadeImage(menuFaded=!menuFaded, 0.5f, menuChildrenAlpha, menuChildrenImg));
        menuImg.enabled = menuFaded ? false : true;
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
        if (!langFaded)
        {
            StartCoroutine(StaticImageFade.FadeImage(langFaded=true, 0.5f, langSelectChildrenAlpha, langSelectChildrenImg));
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
        exitPopup.gameObject.active = true;
    }

    public void OnClickExitConfirm()
    {
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_WEBGL)
            Application.OpenURL("https://gorfmcgorf.itch.io/");
        #endif
    }

    public void OnClickCancel()
    {
        exitPopup.gameObject.active = false;
    }
}
