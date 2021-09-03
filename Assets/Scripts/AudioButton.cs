using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    /**** image assets for audio button and label ****/
    public SpriteRenderer label;
    private float[] allAlphas;
    private SpriteRenderer[] allSprites;
    private SpriteRenderer[] labelSprites;
    private float[] labelAlphas;
    private Animator animator;

    /**** corresponding UI title ****/
    public Image img;
    private float[] imgAlphas;
    private Image[] imgChildren;

    /**** audio ****/
    public AudioSource audio;
    
    /**** making sure shows up at right time ****/
    public bool detectClick;

    // Start is called before the first frame update
    void Start()
    {
        allAlphas = StaticImageFade.GetOriginalAlphas(GetComponent<SpriteRenderer>());
        allSprites = StaticImageFade.GetOriginalImages(GetComponent<SpriteRenderer>());

        labelSprites = StaticImageFade.GetOriginalImages(label);
        labelAlphas= StaticImageFade.GetOriginalAlphas(label);

        imgAlphas = StaticImageFade.GetOriginalAlphas(img);
        imgChildren = StaticImageFade.GetOriginalImages(img);
        
        //StaticImageFade.QuickFade(allSprites);
        img.gameObject.active = true;
        StaticImageFade.QuickFade(imgChildren);


        animator = GetComponent<Animator>();

        detectClick = false;

        StartCoroutine(StaticImageFade.FadeImage(false, 0.5f, allAlphas, allSprites));
        StartCoroutine(StaticImageFade.FadeImage(true, 0.5f, labelAlphas, labelSprites));

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
        audio.Play();
        audio.GetComponent<AudioPause>().started = true;
        yield return StartCoroutine(StaticImageFade.FadeImage(true, 1f, allAlphas, allSprites));
    }

    public IEnumerator DisplayText(float[] alphas, Image[] images)
    {
        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(StaticImageFade.FadeImage(false, 1f, alphas, images));
        yield return new WaitForSeconds(10f);
        yield return StartCoroutine(StaticImageFade.FadeImage(true, 1f, alphas, images));
        detectClick = false;
        transform.gameObject.active = false;
    }

    public IEnumerator OnSkip()
    {
        yield return StartCoroutine(StaticImageFade.FadeImage(true, 0.5f, imgAlphas, imgChildren));
        yield return StartCoroutine(StaticImageFade.FadeImage(true, 0.5f, allAlphas, allSprites));
    }
}
