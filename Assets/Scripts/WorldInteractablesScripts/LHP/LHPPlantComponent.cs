using DG.Tweening;
using UnityEngine;
using System;

public class LHPPlantComponent : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] float spawnDuration;
    [SerializeField] Ease spawnEase;

    [Header("Uppies")]
    [SerializeField] float moveUpHeight;
    [SerializeField] float moveUpDuration;
    [SerializeField] Ease moveUpEase;

    [Header("Despawn")]
    [SerializeField] float despawnDuration;
    [SerializeField] Ease despawnEase;

    LHPPlantSpawner myOwner;
    Tween currentPlantTween;

    public void InitializePlant(LHPPlantSpawner owner)
    {
        myOwner = owner;
    }

    public void StartPlant()
    {
        currentPlantTween?.Complete();

        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);


        currentPlantTween = transform.DOScale(Vector3.one, spawnDuration).SetEase(spawnEase);
    }
    public void PickupPlant()
    {
        currentPlantTween?.Complete();


        currentPlantTween = transform.DOLocalMoveY(transform.localPosition.y + moveUpHeight, moveUpDuration).SetEase(moveUpEase);
    }
    public void CompletePickupAndDisable()
    {
        currentPlantTween?.Complete();

        currentPlantTween = transform.DOScale(Vector3.zero, despawnDuration).SetEase(despawnEase).OnComplete(() => gameObject.SetActive(false));

        DisablePlant();
    }

    public void DisablePlant()
    {
        myOwner.RegisterDisabled(this);
    }
}
