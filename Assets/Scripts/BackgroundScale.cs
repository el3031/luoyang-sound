using UnityEngine;
using UnityEngine.UI;

public class BackgroundScale : MonoBehaviour {
    
    private Image img;
    private float sHeight;
    private float sWidth;

    void Start()
    {
        img = GetComponent<Image>();
        sHeight = img.sprite.bounds.size.y;
        sWidth = img.sprite.bounds.size.x;
    }
    void Update()
    {
        float width = ScreenSize.GetScreenToWorldWidth;
        float height = ScreenSize.GetScreenToWorldHeight;
        float hRatio = height / sHeight;
        float wRatio = width / sWidth;
        transform.localScale =  Vector3.one * ((wRatio > hRatio) ? width : height);
    }
}