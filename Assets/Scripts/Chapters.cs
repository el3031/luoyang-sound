using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chapters : MonoBehaviour
{
    public Image buddhism;
    public Image army;
    public Image fire;
    private float[] buddhismAlpha;
    private Image[] buddhismImage;
    private float[] armyAlpha;
    private Image[] armyImage;
    private float[] fireAlpha;
    private Image[] fireImage;

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
        buddhismImage = GetOriginalImages(buddhism);
        fireImage = GetOriginalImages(fire);
        armyImage = GetOriginalImages(army);

        buddhismAlpha = GetOriginalAlphas(buddhismImage);
        armyAlpha = GetOriginalAlphas(armyImage);
        fireAlpha = GetOriginalAlphas(fireImage);

        fire.gameObject.active = false;
        buddhism.gameObject.active = false;
        army.gameObject.active = false;
    }

    float[] GetOriginalAlphas(Image[] imgChildren)
    {
        float[] imgAlphas = new float[imgChildren.Length];
        imgAlphas[0] = imgChildren[0].color.a;
        for (int i = 1; i < imgChildren.Length; i++)
        {
            imgAlphas[i] = imgChildren[i].color.a;
            Debug.Log(imgAlphas[i]);
        }
        return imgAlphas;
    }

    Image[] GetOriginalImages(Image image)
    {
        Image[] imgChildren = new Image[image.transform.childCount+1];
        imgChildren[0] = image;
        for (int i = 0; i < image.transform.childCount; i++)
        {
            imgChildren[i+1] = image.transform.GetChild(i).GetComponent<Image>();
        }
        return imgChildren;
    }

    public IEnumerator EndOtherChapters(GameObject button, AudioSource audio, Image[] imgChildren, float[] imgAlpha)
    {
        button.active = false;
        audio.Stop();
        audio.GetComponent<AudioPause>().started = false;
        audio.GetComponent<AudioPause>().paused = false;

        if (imgChildren[0].gameObject.active && button.GetComponent<AudioButton>().chapterShowed)
        {
            Debug.Log("fade chapter");
            yield return StartCoroutine(StaticImageFade.FadeImage(true, 1f, imgAlpha, imgChildren));
       }
        yield return null;
    }
    public IEnumerator BuddhismMain()
    {
        buddhismButton.active = true;
        
        StopCoroutine(FireMain());
        StopCoroutine(ArmyMain());
        yield return StartCoroutine(EndOtherChapters(fireButton, fireAudio, fireImage, fireAlpha));
        yield return StartCoroutine(EndOtherChapters(armyButton, armyAudio, armyImage, armyAlpha));
        
        
        while (!buddhismAudio.GetComponent<AudioPause>().started ||
               buddhismAudio.GetComponent<AudioPause>().paused ||
               buddhismAudio.isPlaying)
        {
            yield return null;
        }
        StartCoroutine(ArmyMain());
    }

    public IEnumerator ArmyMain()
    {
        StopCoroutine(FireMain());
        StopCoroutine(BuddhismMain());
        yield return StartCoroutine(EndOtherChapters(buddhismButton, buddhismAudio, buddhismImage, buddhismAlpha));
        yield return StartCoroutine(EndOtherChapters(fireButton, fireAudio, fireImage, fireAlpha));

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
        StopCoroutine(BuddhismMain());
        buddhismAudio.Stop();
        yield return StartCoroutine(buddhismButton.GetComponent<AudioButton>().OnSkip());
        buddhismButton.active = false;

        StopCoroutine(ArmyMain());
        armyAudio.Stop();
        if (armyButton.active)
        {
            yield return StartCoroutine(armyButton.GetComponent<AudioButton>().OnSkip());
        }
        armyButton.active = false;
        
        fireButton.active = true;
        while (!fireAudio.GetComponent<AudioPause>().started ||
               fireAudio.GetComponent<AudioPause>().paused ||
               fireAudio.isPlaying)
        {
            yield return null;
        }
    }

}
