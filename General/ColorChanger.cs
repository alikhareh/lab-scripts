using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Image targetImage;
    public Color newColor = Color.black, defualtColor = Color.white;

    public void ChangeColor()
    {
        if (targetImage != null)
            targetImage.color = newColor;
    }

    public void ResetColor()
    {
        targetImage.color = defualtColor;
    }
}