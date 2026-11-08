using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DotBetweenVecs : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Vector3 EndPosFromLocal;
    [SerializeField] float duration;
    [SerializeField] float startOffsetTime;

    private Vector3 StartPos;
    private Vector3 EndPos;

    void Start()
    {
        EndPos = transform.position + EndPosFromLocal;
        StartPos = transform.position;

        StartCoroutine(StartOffset());
    }

    private IEnumerator StartOffset()
    {
        yield return new WaitForSeconds(startOffsetTime);
        MoveUp();

    }
    private void MoveUp() {
        transform.DOMove(EndPos, duration).OnComplete(MoveDown);
    }
    private void MoveDown() {
        transform.DOMove(StartPos, duration).OnComplete(MoveUp);
    }
}
