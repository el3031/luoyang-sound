using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LangSelect : MonoBehaviour
{
    [SerializeField] private Image chSelected;
    [SerializeField] private Image enSelected; 
    public StartMenu StartMenu;

    public Chapters Chapters;
    public string scene;
    
    // Start is called before the first frame update
    void Start()
    {
        ChangeSelectedLang();
    }

    // Update is called once per frame
    public void OnClickEN()
    {
        PlayerPrefs.SetString("language", "en");
        ChangeSelectedLang();
        
    }

    public void OnClickCH()
    {
        PlayerPrefs.SetString("language", "ch");
        ChangeSelectedLang();
    }

    public void ChangeSelectedLang()
    {
        if (PlayerPrefs.GetString("language") == "en")
        {
            enSelected.gameObject.active = true;
            chSelected.gameObject.active = false;
        }
        else
        {
            chSelected.gameObject.active = true;
            enSelected.gameObject.active = false;
        }
        NextScene(PlayerPrefs.GetString("language"));
    }

    void NextScene(string language)
    {
        if ((SceneManager.GetActiveScene().name == "yongningsi" && language == "en")
            || (SceneManager.GetActiveScene().name == "yongningsi-EN" && language == "ch"))
        {
            SceneManager.LoadScene(scene);
        }
    }
}
