using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class StaticImageFade : MonoBehaviour {
   
    public static float[] GetOriginalAlphas(Image img)
    {
        float[] alphas = new float[img.transform.childCount+1];
        alphas[0] = img.color.a;

        int i = 1;
        foreach (Transform child in img.transform)
        {
            alphas[i++] = child.gameObject.GetComponent<Image>().color.a;
        }
        return alphas;
    }

    public static Image[] GetOriginalImages(Image img)
    {
        Image[] images = new Image[img.transform.childCount+1];
        images[0] = img;

        int i = 1;
        foreach (Transform child in img.transform)
        {
            images[i++] = child.gameObject.GetComponent<Image>();
        }
        return images;
    }

    public static void QuickFade(Image[] images)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 0);
        }
    }

    public static void QuickShow(Image[] images)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 1);
        }
    }
 
    public static IEnumerator FadeImage(bool fadeAway, float time, float[] alphas, Image[] images)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = time; i >= 0; i -= Time.deltaTime)
            {
                for (int j = 0; j < images.Length; j++)
                {
                    images[j].color = new Color(images[j].color.r, images[j].color.g, images[j].color.b, alphas[j] * i/time);
                }
                yield return null;
            }
            for (int k = 0; k < images.Length; k++)
            {
                images[k].color = new Color(images[k].color.r, images[k].color.g, images[k].color.b, 0f);
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= time; i += Time.deltaTime)
            {
                for (int j = 0; j < images.Length; j++)
                {
                    images[j].color = new Color(images[j].color.r, images[j].color.g, images[j].color.b, i/time * alphas[j]);
                }
                yield return null;
            }
        }
        
    }
}