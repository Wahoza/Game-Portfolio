using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using DG.Tweening;

[RequireComponent(typeof(PlayerModelControllerComponent))]
public class NunComponent : MonoBehaviour
{
    PlayerModelControllerComponent controller;

    [SerializeField] float walkSpeed;
    [SerializeField] float grabDist;
    [SerializeField] float waitUntilGrab;
    [SerializeField] float grabDuration;
    [SerializeField] float releaseDuration;

    private void Awake()
    {
        controller = GetComponent<PlayerModelControllerComponent>();
    }

    public IEnumerator WalkToAndPickUp(Transform target, Action pickUpAction, Action completePickupAction)
    {
        Vector3 startingPos = transform.position;
        Vector3 targetPos = target.position;

        targetPos.y = startingPos.y;

        transform.DOLookAt(targetPos, 0.1f);
        
        float dist = Vector3.Distance(startingPos, targetPos);


        float walkDuration = dist / walkSpeed;


        dist = Mathf.Clamp(dist - grabDist, 0, dist);
        float walkGrabRangeDuration = dist / walkSpeed;
        
        walkDuration = Mathf.Clamp(walkDuration, 0.00001f, walkDuration);
        walkGrabRangeDuration = Mathf.Clamp(walkGrabRangeDuration, 0.00001f, walkGrabRangeDuration);

        float timeInWalk = 0;

        controller.UpdateMoveSpeed(1);

        while (true)
        {
            float completionAmountWalk = timeInWalk / walkDuration;
            float completionAmountGrab = timeInWalk / walkGrabRangeDuration;

            if (completionAmountGrab > 1)
            {
                controller.UpdateMoveSpeed(0);

                yield return StartCoroutine(PickUpRoutine(pickUpAction, completePickupAction));

                yield break;
            }

            timeInWalk += Time.deltaTime;

            Vector3 reachedPos = Vector3.Lerp(startingPos, targetPos, completionAmountWalk);

            transform.position = reachedPos;

            yield return null;
        }
    }

    public IEnumerator PickUpRoutine(Action pickUpAction, Action completePickupAction)
    {
        yield return new WaitForSeconds(waitUntilGrab);

        pickUpAction.Invoke();
        controller.OnGrab(true);

        yield return new WaitForSeconds(grabDuration);

        completePickupAction.Invoke();
        controller.OnGrab(false);

        yield return new WaitForSeconds(releaseDuration);

        yield return null;
    }
}
