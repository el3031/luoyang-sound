using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    /**** image assets for audio button and label ****/
    public SpriteRenderer label;
    public float[] allAlphas;
    private SpriteRenderer[] allSprites;
    private SpriteRenderer[] labelSprites;
    public float[] labelAlphas;
    private Animator animator;

    /**** corresponding UI title ****/
    public Image img;
    public float[] imgAlphas;
    public Image[] imgChildren;

    /**** audio ****/
    public AudioSource audio;

    public bool chapterShowed;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        imgAlphas = StaticImageFade.GetOriginalAlphas(img);
        imgChildren = StaticImageFade.GetOriginalImages(img);
        StaticImageFade.QuickFade(imgChildren);
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        Debug.Log("enable" + gameObject.name);
        allAlphas = new float[2]{1f, 1f};
        allSprites = StaticImageFade.GetOriginalImages(GetComponent<SpriteRenderer>());

        labelSprites = StaticImageFade.GetOriginalImages(label);
        labelAlphas= new float[1]{1f};
                
        img.gameObject.active = true;
        label.gameObject.active = true;
        StaticImageFade.QuickFade(imgChildren);
        StartCoroutine(StaticImageFade.FadeImage(false, 0.5f, allAlphas, allSprites));
        StartCoroutine(StaticImageFade.FadeImage(true, 0.5f, labelAlphas, labelSprites));
        chapterShowed = false;
    }

    void Update()
    {
        
        DetectClick();
    }

    void DetectClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
            RaycastHit hit;  
            if (Physics.Raycast(ray, out hit)) {  
                Debug.Log(hit.transform.name);
                //Select stage    
                if (hit.transform.name == this.gameObject.name) {
                    animator.SetBool("click", true);
                    StartCoroutine(OnClick());
                }
            }
        }
    }

    void OnMouseEnter()
    {
        StartCoroutine(StaticImageFade.FadeImage(false, 0.5f, labelAlphas, labelSprites));
        animator.SetBool("hovering", true);
        
    }

    void OnMouseExit()
    {
        StartCoroutine(StaticImageFade.FadeImage(true, 0.5f, labelAlphas, labelSprites));
        animator.SetBool("hovering", false);  
    }

    public IEnumerator OnClick()
    {
        StartCoroutine(DisplayText(imgAlphas, imgChildren));
        yield return StartCoroutine(StaticImageFade.FadeImage(true, 0.5f, labelAlphas, labelSprites));
        label.gameObject.active = false;
        audio.gameObject.active = true;
        audio.Play();
        audio.GetComponent<AudioPause>().started = true;
        yield return StartCoroutine(StaticImageFade.FadeImage(true, 1f, allAlphas, allSprites));
    }

    public IEnumerator DisplayText(float[] alphas, Image[] images)
    {
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(StaticImageFade.FadeImage(false, 1f, alphas, images));
        chapterShowed = true;
        yield return new WaitForSeconds(10f);
        yield return StartCoroutine(StaticImageFade.FadeImage(true, 1f, alphas, images));
        chapterShowed = false;
        transform.gameObject.active = false;
        img.gameObject.active = false;
    }

    public IEnumerator OnSkip()
    {
        yield return StartCoroutine(StaticImageFade.FadeImage(true, 0.5f, imgAlphas, imgChildren));
        yield return StartCoroutine(StaticImageFade.FadeImage(true, 0.5f, allAlphas, allSprites));
    }
}
