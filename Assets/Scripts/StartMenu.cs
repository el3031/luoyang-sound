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
    
    void Start()
    {
        menuFaded=false;
        menuChildrenImg = StaticImageFade.GetOriginalImages(menuImg);
        menuChildrenAlpha = StaticImageFade.GetOriginalAlphas(menuImg);
    }
    public void OnClickMenu()
    {
        StartCoroutine(StaticImageFade.FadeImage(menuFaded=!menuFaded, 0.5f, menuChildrenAlpha, menuChildrenImg));
    }
    
    public void OnClickBuddhism()
    {
        PlayerPrefs.SetString("chapter", "buddhism");
        SceneManager.LoadScene("yongningsi");
    }

    public void OnClickArmy()
    {
        PlayerPrefs.SetString("chapter", "army");
        SceneManager.LoadScene("yongningsi");

    }

    public void OnClickSoundlib()
    {
        PlayerPrefs.SetString("chapter", "soundlib");
        SceneManager.LoadScene("yongningsi");
    }

    public void OnClickFire()
    {
        PlayerPrefs.SetString("chapter", "fire");
        SceneManager.LoadScene("yongningsi");
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
