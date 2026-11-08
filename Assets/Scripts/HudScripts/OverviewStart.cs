using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OverviewStart : MonoBehaviour
{
    public void StartHide()
    {
        transform.DOMoveY(transform.position.y + 30, 0.2f).OnComplete(FinishHide);
    }

    private void FinishHide()
    {
        GameManager.Instance.StartGame();

        float time = 0.4f;
        transform.DOMoveY(transform.position.y - 500, time).OnComplete(()=>gameObject.SetActive(false));
        transform.DOScale(0.3f, time);
    }
}
