using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myText;
    [SerializeField] RawImage myImage;
    [SerializeField] float duration;
    public void HideObject()
    {
        myText.DOColor(new Color(1, 1, 1, 0), duration);
        myImage.DOColor(new Color(1, 1, 1, 0), duration);
    }    
    public void ShowObject()
    {
        myText.DOColor(new Color(1, 1, 1, 1), duration);
        myImage.DOColor(new Color(1, 1, 1, 1), duration);
    }
}
