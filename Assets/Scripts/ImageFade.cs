using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class ImageFade : MonoBehaviour {
 
    // the image you want to fade, assign in inspector
    public Image img;
    private bool faded;
    private float originalAlpha;
    private float[] childAlphas;
   
    void Start()
    {
        originalAlpha = img.color.a;
        childAlphas = new float[img.transform.childCount];
        int i = 0;
        foreach (Transform child in img.transform)
        {
            childAlphas[i++] = child.gameObject.GetComponent<Image>().color.a;
        }
    }
    
    public void OnButtonClick()
    {
        // fades the image out when you click
        StartCoroutine(FadeImage(!faded, 1f));
    }
 
    IEnumerator FadeImage(bool fadeAway, float time)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = time; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                float currentAlpha = img.color.a;
                img.color = new Color(1, 1, 1, currentAlpha - Time.deltaTime);
                foreach (Transform child in img.transform)
                {
                    child.gameObject.GetComponent<Image>().color = img.color;
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
                int j = 0;
                foreach (Transform child in img.transform)
                {
                    child.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, i/time * childAlphas[j++]);
                }
                yield return null;
            }
            img.color = new Color(1, 1, 1, originalAlpha);
        }
        faded = !faded;
    }
}