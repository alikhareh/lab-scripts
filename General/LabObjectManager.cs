using TMPro;
using UnityEngine;

public class LabObjectManager : MonoBehaviour
{
    public string name;

    public GameObject popUp;
    public TextMeshProUGUI popUpBodyText, popUpTitleText;
    
    public void ShowPopUp()
    {
        popUp.SetActive(true);
    }

    public void HidePopUp()
    {
        popUp.SetActive(false);
    }
    
    public void EditPopUp(string bodyText)
    {
        popUpBodyText.text = bodyText;
        popUpTitleText.text = name;
    }
}
