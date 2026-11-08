using DG.Tweening;
using UnityEngine;

public class InteractAppearScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Transform hintObj;
    [SerializeField] float hintAnimDuration;
    [SerializeField] Transform interactObj;
    [SerializeField] float interactionAnimDuration;

    void Start()
    {
        hintObj.gameObject.SetActive(false);
        interactObj.gameObject.SetActive(false);

        hintObj.localScale = Vector3.zero;
        interactObj.localScale = Vector3.zero;
    }
    
    public void AppearStart()
    {
        interactObj.gameObject.SetActive(true);
        interactObj.DOScale(Vector3.one, interactionAnimDuration).OnComplete(AppearHint);
    }
    private void AppearHint()
    {
        hintObj.gameObject.SetActive(true);
        hintObj.localScale = new Vector3(0f, 1.0f, 1.0f);
        hintObj.DOScaleX(1, interactionAnimDuration);
    }

    public void Hide()
    {
        interactObj.DOScale(Vector3.zero, hintAnimDuration);
        hintObj.DOScale(Vector3.zero, hintAnimDuration);
    }
}
