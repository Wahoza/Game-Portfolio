using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MouthHudMovement : MonoBehaviour
{
    [SerializeField] Transform upperMouth;
    [SerializeField] Transform lowerMouth;
    [SerializeField] GameObject mouthHolder;

    [SerializeField] float yOffset;
    [SerializeField] float moveInTime;
    [SerializeField] float holdTime;

    private float lowestYUpper = 0f;
    private float highestYUpper = 0f;    
    private float lowestYLower = 0f;
    private float highestYLower = 0f;
    private void Start()
    {
        mouthHolder.SetActive(false);
        lowestYUpper = upperMouth.localPosition.y;
        highestYLower = lowerMouth.localPosition.y;

        upperMouth.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + yOffset, 0f);
        lowerMouth.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - yOffset, 0f);

        highestYUpper = upperMouth.localPosition.y;
        lowestYLower = lowerMouth.localPosition.y;

    }
    public void DoAnim()
    {
        mouthHolder.SetActive(true);

        upperMouth.DOLocalMoveY(lowestYUpper, moveInTime);
        lowerMouth.DOLocalMoveY(highestYLower, moveInTime).OnComplete(() => AudioManager.Instance.PlaySFX("Chomp"));

        StartCoroutine(FinishWaitAnim());
    }


    private IEnumerator FinishWaitAnim()
    {
        yield return new WaitForSeconds(holdTime + moveInTime);

        FinishAnim();
    }

    private void FinishAnim()
    {
        Debug.Log(highestYUpper);

        AudioManager.Instance.PauseMusic();
        upperMouth.DOLocalMoveY(highestYUpper, moveInTime).OnComplete(()=> AudioManager.Instance.PlayBoatSounds());
        lowerMouth.DOLocalMoveY(lowestYLower, moveInTime).OnComplete(() => mouthHolder.SetActive(false));
    }
    private void OnTriggerEnter(Collider other)
    {
        string name = other.name;

        if(name == "Boat")
        {
            DoAnim();
        }
    }
}
