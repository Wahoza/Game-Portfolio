using DG.Tweening;
using UnityEngine;

public class PanelItemScript : MonoBehaviour
{
    [SerializeField] float yOffset;
    [SerializeField] float moveInTime;

    [SerializeField] private bool isHidden;
    private float lowestY = 0f; 
    private float highestY = 0f; 
    private bool isInAnim = false;
    private void Start()
    {
        highestY = transform.localPosition.y;
        lowestY = lowestY - yOffset;
        if (isHidden)
        {
            gameObject.SetActive(false);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - yOffset, 0f);
        }

    }
    public void SwitchHiddenState()
    {
        if (isHidden)
        {
            Show();
            return;
        }
        Hide();
    }
    public void Show()
    {
        if (!(isHidden && !isInAnim)) return;
        AudioManager.Instance.PlaySFX("Click");
        AudioManager.Instance.PauseBoatSounds();

        isHidden = false;
        gameObject.SetActive(true);
        transform.DOLocalMoveY(0, moveInTime).OnComplete(CompleteShow);
        isInAnim = true;
        
    }

    public void Hide()
    {
        if (!(!isHidden && !isInAnim)) return;
        AudioManager.Instance.PlaySFX("Click");
        AudioManager.Instance.PlayBoatSounds();

        isHidden = true;
        transform.DOLocalMoveY(lowestY, moveInTime).OnComplete(CompleteHide);
        isInAnim = true;

        GameManager.Instance.RequestUnpause();
    }

    private void CompleteHide()
    {
        gameObject.SetActive(false);
        isInAnim = false;
    }
    private void CompleteShow()
    {
        GameManager.Instance.RequestPause();
        isInAnim = false;
    }
}
