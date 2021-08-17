using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class ImageFade : MonoBehaviour {
 
    // the image you want to fade, assign in inspector
    public Image img;
    private bool faded;
    private float originalAlpha;
    private float[] childAlphas;
    private Image[] childImages;
   
    void Start()
    {
        originalAlpha = img.color.a;
        childAlphas = new float[img.transform.childCount];
        childImages = new Image[childAlphas.Length];
        int i = 0;
        foreach (Transform child in img.transform)
        {
            childImages[i] = child.gameObject.GetComponent<Image>();
            childAlphas[i] = childImages[i].color.a;
            i++;
        }
    }
    
    public void FadeUIMenu()
    {
        // fades the image out when you click
        StartCoroutine(FadeImage(!faded, 0.5f));
        faded = !faded;
    }
 
    public IEnumerator FadeImage(bool fadeAway, float time)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = time; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                float currentAlpha = img.color.a;
                img.color = new Color(1, 1, 1, originalAlpha * i/time);
                
                for (int j = 0; j < childImages.Length; j++)
                {
                    childImages[j].color = img.color;
                }
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= time; i += Time.deltaTime)
            {
                // set color with i as alpha
                float currentAlpha = img.color.a;
                img.color = new Color(1, 1, 1, i/time * originalAlpha);
                
                for (int j = 0; j < childImages.Length; j++)
                {
                    childImages[j].color = new Color(1, 1, 1, i/time * childAlphas[j]);
                }
                yield return null;
            }
            img.color = new Color(1, 1, 1, originalAlpha);
        }
        
    }
}