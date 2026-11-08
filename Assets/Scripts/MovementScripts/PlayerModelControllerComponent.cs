using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class PlayerModelControllerComponent : MonoBehaviour
{
    [Header("ObjectsToAnimate")]
    [SerializeField] GameObject HandL;
    [SerializeField] GameObject HandR;
    [SerializeField] GameObject FootL;
    [SerializeField] GameObject FootR;
    [SerializeField] GameObject Body;
    [SerializeField] GameObject Eyes;
    [SerializeField] GameObject ModelHolder;

    [Header("Foot Animation Settings")]
    [SerializeField] Vector3 startFootRotation;
    [SerializeField] Vector3 endFootRotation;
    [SerializeField] float baseFootDuration;
    [SerializeField] float moveSpeedFootImpact;

    private Coroutine playerFootWalkRoutine;
    private Coroutine footLRoutine;
    private Coroutine footRRoutine;

    [Header("Hand Animation Settings")]
    [SerializeField] Vector3 startHandRotation;
    [SerializeField] Vector3 endHandRotation;
    [SerializeField] float baseHandDuration;
    [SerializeField] float moveSpeedHandImpact;

    private Coroutine playerHandWalkRoutine;
    private Coroutine handLRotateRoutine;
    private Coroutine handRRotateRoutine;
    private Coroutine handRMoveRoutine;
    private Coroutine handLMoveRoutine;

    [Header("Grab Animation Settings")]
    [SerializeField] Vector3 startGrabRotation;
    [SerializeField] Vector3 endGrabRotation;
    [SerializeField] Vector3 startGrabPosition;
    [SerializeField] Vector3 endGrabPosition;
    [SerializeField] float baseGrabDuration;

    private Coroutine playerGrabRoutine;
    private Coroutine playerReleaseRoutine;
    private bool lastGrabbed;

    [Header("Attack Animation Settings")]
    [SerializeField] Vector3 startAttackRotation;
    [SerializeField] Vector3 endAttackRotation;
    [SerializeField] Vector3 startAttackPosition;
    [SerializeField] Vector3 endAttackPosition;
    [SerializeField] float attackAnimDuration;
    [SerializeField] float attackHoldDuration;
    [SerializeField] float attackMoveBackDuration;

    private Coroutine playerAttackRoutine;

    [Header("Jump Animation Settings")]
    [SerializeField] Vector3 startJumpRotation;
    [SerializeField] Vector3 endJumpRotation;
    [SerializeField] float baseJumpDuration;
    [SerializeField] float jumpHoldDuration;

    private Coroutine playerJumpRoutine;

    [Header("Body Animation Settings")]
    [SerializeField] Vector3 startBodyPosition;
    [SerializeField] Vector3 endBodyPosition;
    [SerializeField] float bobAnimDuration;
    [SerializeField] float bobAnimMSImpact;

    private Coroutine bodyAnimRoutine;
    private Coroutine bodyBobRoutine;

    [Header("Body Landing Animation Settings")]
    [SerializeField] Vector3 startLandBodyPosition;
    [SerializeField] Vector3 endLandBodyPosition;
    [SerializeField] float landAnimDuration;
    [SerializeField] float airTimeDurationImpact;
    [SerializeField] float landHoldDuration;
    [SerializeField] float popUpDuration;
    [SerializeField] float airTimeMin;

    private Coroutine bodyLandingRoutine;

    [Header("AnimationSettings")]
    [SerializeField] AnimationCurve animEasing = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private float moveSpeedMin;
    [SerializeField] private float animationSpeed = 1;

    [Header("HighlightBehindTerrainObjects")]
    [SerializeField] List<MeshRenderer> highlightMeshes;
    [SerializeField] Material highlightMat;
    [SerializeField] float darkenStrength;

    [Header("Other HighlightThings")]
    [SerializeField] DecalProjector decalProjector;
    [SerializeField] float decalOpacity;
    [SerializeField] List<GameObject> winIndicators;

    private float moveSpeed;
    private Material mat;
    internal void Start()
    {
        if(playerFootWalkRoutine == null)
        {
            playerFootWalkRoutine = StartCoroutine(WalkFootRoutine());
        }

        if(playerHandWalkRoutine == null)
        {
            playerHandWalkRoutine = StartCoroutine(WalkHandRoutine());
        }

        if(bodyAnimRoutine == null)
        {
            bodyAnimRoutine = StartCoroutine(HeadBobRoutine());
        }
    }
    internal void ResetComponent()
    {
        StopHandCoroutines();

        if(playerFootWalkRoutine != null)
        {
            StopCoroutine(playerFootWalkRoutine);
            playerFootWalkRoutine = null;
        }
        playerFootWalkRoutine = StartCoroutine(WalkFootRoutine());

        if(playerHandWalkRoutine != null)
        {
            StopCoroutine(playerHandWalkRoutine);
            playerHandWalkRoutine = null;
        }
        playerHandWalkRoutine = StartCoroutine(WalkHandRoutine());

        if(bodyAnimRoutine != null)
        {
            StopCoroutine(bodyAnimRoutine);
            bodyAnimRoutine = null;
        }
        bodyAnimRoutine = StartCoroutine(HeadBobRoutine());
        lastGrabbed = false;
    }
    IEnumerator HeadBobRoutine()
    {
        while (true)
        {
            bodyBobRoutine = StartCoroutine(LerpWalkPositionValue(Body, startBodyPosition, endBodyPosition, bobAnimDuration, bobAnimMSImpact));
            yield return bodyBobRoutine;


            bodyBobRoutine = StartCoroutine(LerpWalkPositionValue(Body, endBodyPosition, startBodyPosition, bobAnimDuration, bobAnimMSImpact));
            yield return bodyBobRoutine;
        }
    }
    IEnumerator WalkHandRoutine()
    {
        while (true)
        {
            // Forward
            handRRotateRoutine = StartCoroutine(LerpWalkRotationValue(HandR, endHandRotation, startHandRotation, baseHandDuration, moveSpeedHandImpact));
            handLRotateRoutine = StartCoroutine(LerpWalkRotationValue(HandL, startHandRotation, endHandRotation, baseHandDuration, moveSpeedHandImpact));
            yield return handLRotateRoutine;

            // Backward
            handRRotateRoutine = StartCoroutine(LerpWalkRotationValue(HandR, startHandRotation, endHandRotation, baseHandDuration, moveSpeedHandImpact));
            handLRotateRoutine = StartCoroutine(LerpWalkRotationValue(HandL, endHandRotation, startHandRotation, baseHandDuration, moveSpeedHandImpact));
            yield return handLRotateRoutine;
        }
    }
    IEnumerator WalkFootRoutine()
    {
        while (true)
        {
            // Forward
            footRRoutine = StartCoroutine(LerpWalkRotationValue(FootR, endFootRotation, startFootRotation, baseFootDuration, moveSpeedFootImpact));
            footLRoutine = StartCoroutine(LerpWalkRotationValue(FootL, startFootRotation, endFootRotation, baseFootDuration, moveSpeedFootImpact));

            yield return footLRoutine;
            // Backward
            footRRoutine = StartCoroutine(LerpWalkRotationValue(FootR, startFootRotation, endFootRotation, baseFootDuration, moveSpeedFootImpact));
            footLRoutine = StartCoroutine(LerpWalkRotationValue(FootL, endFootRotation, startFootRotation, baseFootDuration, moveSpeedFootImpact));

            yield return footLRoutine;
        }
    }

    IEnumerator LerpWalkRotationValue(GameObject goToAnimate, Vector3 from, Vector3 to, float time, float msImpact)
    {
        float elapsed = 0f;

        while (elapsed < time / Mathf.Clamp(animationSpeed, 0.0001f, float.MaxValue))
        {
            if (moveSpeed > moveSpeedMin)
            {
                float t = elapsed / time;
                t = animEasing.Evaluate(t);

                goToAnimate.transform.localRotation = Quaternion.Euler(Vector3.Lerp(from, to, t));

                elapsed += Time.deltaTime * moveSpeed * msImpact * animationSpeed;
            }
            else
            {
                elapsed = 0.3f;

                goToAnimate.transform.localRotation = Quaternion.identity;
            }
                yield return null;
        }

        goToAnimate.transform.localRotation = Quaternion.Euler(to);
    }

    IEnumerator LerpWalkPositionValue(GameObject goToAnimate, Vector3 from, Vector3 to, float time, float msImpact)
    {
        float elapsed = 0f;

        while (elapsed < time / Mathf.Clamp(animationSpeed, 0.0001f, float.MaxValue))
        {
            if (moveSpeed > moveSpeedMin)
            {
                float t = elapsed / time;
                goToAnimate.transform.localPosition = Vector3.Lerp(from, to, t);

                elapsed += Time.deltaTime * msImpact * moveSpeed * animationSpeed;
            }

            yield return null;
        }

        goToAnimate.transform.localPosition = to;
    }

    IEnumerator HandGrabRoutine()
    {
        handLRotateRoutine = StartCoroutine(RotateRoutine(HandL, startGrabRotation, endGrabRotation, baseGrabDuration));
        handRRotateRoutine = StartCoroutine(RotateRoutine(HandR, startGrabRotation, endGrabRotation, baseGrabDuration));

        handLMoveRoutine = StartCoroutine(LerpPositionValue(HandL, startGrabPosition, endGrabPosition, baseGrabDuration));
        handRMoveRoutine = StartCoroutine(LerpPositionValue(HandR, startGrabPosition, endGrabPosition, baseGrabDuration));
        yield return null;
    }

    IEnumerator HandReleaseRoutine()
    {
        handLRotateRoutine = StartCoroutine(RotateRoutine(HandL, endGrabRotation, startGrabRotation, baseGrabDuration));
        handRRotateRoutine = StartCoroutine(RotateRoutine(HandR, endGrabRotation, startGrabRotation, baseGrabDuration));

        handLMoveRoutine = StartCoroutine(LerpPositionValue(HandL, endGrabPosition, startGrabPosition, baseGrabDuration));
        handRMoveRoutine = StartCoroutine(LerpPositionValue(HandR, endGrabPosition, startGrabPosition, baseGrabDuration));

        yield return handRRotateRoutine;

        if (playerHandWalkRoutine == null)
        {
            playerHandWalkRoutine = StartCoroutine(WalkHandRoutine());
        }
    }
    IEnumerator LerpPositionValue(GameObject goToAnimate, Vector3 from, Vector3 to, float time)
    {
        float elapsed = 0f;

        while (elapsed < time / Mathf.Clamp(animationSpeed, 0.0001f, float.MaxValue))
        {
            
            float t = elapsed / time;
            goToAnimate.transform.localPosition = Vector3.Lerp(from, to, t);

            elapsed += Time.deltaTime * animationSpeed;
            

            yield return null;
        }
        goToAnimate.transform.localPosition = to;
    }
    IEnumerator LandRoutine(float animTime) 
    {
        yield return StartCoroutine(LerpPositionValue(Body, startLandBodyPosition, endLandBodyPosition, landAnimDuration));

        if (animationSpeed != 0f)
        {
            yield return new WaitForSeconds((landHoldDuration + animTime) / animationSpeed);
        }
        else
        {
            yield return new WaitForSeconds(float.MaxValue);
        }

        yield return StartCoroutine(LerpPositionValue(Body, endLandBodyPosition, startLandBodyPosition, popUpDuration));

        bodyLandingRoutine = null;

        if (bodyAnimRoutine == null)
        {
            bodyAnimRoutine = StartCoroutine(HeadBobRoutine());
        }
    }
    IEnumerator RotateRoutine(GameObject goToAnimate, Vector3 from, Vector3 to, float time) { 
        
        float elapsed = 0f;

        while (elapsed < time / Mathf.Clamp(animationSpeed, 0.0001f, float.MaxValue))
        {
            
            float t = elapsed / time;
            t = animEasing.Evaluate(t);

            goToAnimate.transform.localRotation = Quaternion.Euler(Vector3.Lerp(from, to, t));

            elapsed += Time.deltaTime * animationSpeed;
        

            yield return null;
        }

        goToAnimate.transform.localRotation = Quaternion.Euler(to);
    }
    public void UpdateMoveSpeed(float newMoveSpeed)
    {
        moveSpeed = newMoveSpeed;
    }

    private IEnumerator JumpRoutine() 
    {
        footLRoutine = StartCoroutine(RotateRoutine(FootL, startJumpRotation, endJumpRotation, baseJumpDuration));

        yield return footRRoutine = StartCoroutine(RotateRoutine(FootR, startJumpRotation, endJumpRotation, baseJumpDuration));

        if (animationSpeed != 0f)
        {
            yield return new WaitForSeconds(jumpHoldDuration / animationSpeed);
        }
        else
        {
            yield return new WaitForSeconds(float.MaxValue);
        }

        if (playerFootWalkRoutine == null)
        {
            playerFootWalkRoutine = StartCoroutine(WalkFootRoutine());
        }

        playerJumpRoutine = null;
    }

    private IEnumerator AttackRoutine()
    {
        handLRotateRoutine = StartCoroutine(RotateRoutine(HandL, startAttackRotation, endAttackRotation, attackAnimDuration));
        handRRotateRoutine = StartCoroutine(RotateRoutine(HandR, startAttackRotation, endAttackRotation, attackAnimDuration));

        handLMoveRoutine = StartCoroutine(LerpPositionValue(HandL, startAttackPosition, endAttackPosition, attackAnimDuration));
        handRMoveRoutine = StartCoroutine(LerpPositionValue(HandL, startAttackPosition, endAttackPosition, attackAnimDuration));

        yield return handLRotateRoutine;

        yield return new WaitForSeconds(attackHoldDuration);

        handLRotateRoutine = StartCoroutine(RotateRoutine(HandL, endAttackRotation, startAttackRotation, attackMoveBackDuration));
        handRRotateRoutine = StartCoroutine(RotateRoutine(HandR, endAttackRotation, startAttackRotation, attackMoveBackDuration));

        handLMoveRoutine = StartCoroutine(LerpPositionValue(HandL, endAttackPosition, startAttackPosition, attackAnimDuration));
        handRMoveRoutine = StartCoroutine(LerpPositionValue(HandR, endAttackPosition, startAttackPosition, attackAnimDuration));

        yield return handLRotateRoutine;

        if (playerHandWalkRoutine == null)
        {
            playerHandWalkRoutine = StartCoroutine(WalkHandRoutine());
        }

        playerAttackRoutine = null;
    }
    private IEnumerator GrabReleaseRoutine()
    {
        yield return HandGrabRoutine();
        playerReleaseRoutine = StartCoroutine(HandReleaseRoutine());
    }
    public void OnGrab(bool grabbed)
    {
        if (grabbed != lastGrabbed)
        {
            if (grabbed)
            {
                StopHandCoroutines();

                if(playerHandWalkRoutine != null)
                {
                    StopCoroutine(playerHandWalkRoutine);
                    playerHandWalkRoutine = null;
                }
                playerGrabRoutine = StartCoroutine(HandGrabRoutine());

            }
            else
            {
                if (playerHandWalkRoutine != null)
                {
                    StopCoroutine(playerHandWalkRoutine);
                    playerHandWalkRoutine = null;
                }

                if (playerGrabRoutine == null)
                {
                    StopHandCoroutines();

                    playerReleaseRoutine = StartCoroutine(HandReleaseRoutine());
                }
                else
                {
                    playerReleaseRoutine = StartCoroutine(HandReleaseRoutine());
                }

            }

            lastGrabbed = grabbed;

        }
    }

    public void OnJump()
    {
        if(playerJumpRoutine == null)
        {
            if (playerFootWalkRoutine != null)
            {
                StopCoroutine(playerFootWalkRoutine);
                playerFootWalkRoutine = null;
            }

            if (footLRoutine != null)
            {
                StopCoroutine(footLRoutine);
                StopCoroutine(footRRoutine);

                footLRoutine = null;
                footRRoutine = null;
            }

            playerJumpRoutine = StartCoroutine(JumpRoutine());
        }
    }

    public void OnLand(float airTime) 
    {
        if(airTime <= airTimeMin)
            return;

        Debug.Log("AirTime + " + airTime);
        airTime -= airTimeMin;
        if(bodyLandingRoutine == null)
        {
            if (bodyAnimRoutine != null)
            {
                StopCoroutine(bodyAnimRoutine);
                bodyAnimRoutine = null;
            }

            if(bodyBobRoutine != null)
            {
                StopCoroutine (bodyBobRoutine);
                bodyBobRoutine = null;
            }

            bodyLandingRoutine = StartCoroutine(LandRoutine(airTime * airTimeDurationImpact));
        }
    }

    public void OnAttack()
    {
        Debug.Log(lastGrabbed);

        if (lastGrabbed)
        {
            return;
        }

        if (playerAttackRoutine == null)
        {
            StopHandCoroutines();

            if (playerHandWalkRoutine != null)
            {
                StopCoroutine(playerHandWalkRoutine);
                playerHandWalkRoutine = null;

            }

            playerAttackRoutine = StartCoroutine(AttackRoutine());
        }
    }

    void StopHandCoroutines()
    {
        if (handLRotateRoutine != null)
        {
            StopCoroutine(handLRotateRoutine);
            StopCoroutine(handRRotateRoutine);
            handLRotateRoutine = null;
            handRRotateRoutine = null;
        }

        if (handRMoveRoutine != null)
        {
            StopCoroutine(handRMoveRoutine);
            StopCoroutine(handLMoveRoutine);
            handRMoveRoutine = null;
            handLMoveRoutine = null;
        }

        if(playerGrabRoutine != null)
        {
            StopCoroutine(playerGrabRoutine);
            playerGrabRoutine = null;
        }

        if(playerAttackRoutine != null)
        {
            StopCoroutine(playerAttackRoutine);
            playerAttackRoutine = null;
        }

        if(playerReleaseRoutine != null)
        {
            StopCoroutine(playerReleaseRoutine);
            playerReleaseRoutine = null;
        }

        HandL.transform.localPosition = Vector3.zero;
        HandR.transform.localPosition = Vector3.zero;
        HandL.transform.localRotation = Quaternion.identity;
        HandR.transform.localRotation = Quaternion.identity;
    }

    public void SetColor(Color color)
    {
        if(mat == null)
        {
            var bodyRenderer = Body.GetComponent<MeshRenderer>();
            Material[] mats = bodyRenderer.materials;

            // Clone the first material
            mat = new Material(mats[0]);
            mat.color = color;
            mats[0] = mat;

            Debug.Log("Setting new material");

            // Apply updated materials array to each mesh
            Body.GetComponent<MeshRenderer>().materials = mats;
            Eyes.GetComponent<MeshRenderer>().materials = mats;
            HandL.GetComponent<MeshRenderer>().materials = mats;
            HandR.GetComponent<MeshRenderer>().materials = mats;
            FootL.GetComponent<MeshRenderer>().materials = mats;
            FootR.GetComponent<MeshRenderer>().materials = mats;

            Material[] highlightMats = bodyRenderer.materials;
            var newhighlightMat = new Material(highlightMat);

            newhighlightMat.SetColor("_Color", color);
            newhighlightMat.SetColor("_SecondaryColor", color * darkenStrength);
            highlightMats[0] = newhighlightMat;

            for(int i = 0; i < highlightMats.Length; i++)
            {
                highlightMats[i] = newhighlightMat;
            }
            foreach(var renderer in highlightMeshes)
            {
                renderer.materials = highlightMats;
            }

            Material decalMat = decalProjector.material;
            var newDecalMat = new Material(decalMat);
            var decalColor = color;
            decalColor.a = decalOpacity;

            newDecalMat.SetColor("_Color", decalColor);

            decalProjector.material = newDecalMat;
            Debug.Log("Set Color of decal Projector to" + color);
        }

    }

    public void ShowWinObjects()
    {
        foreach (GameObject g in winIndicators)
        {
            g.SetActive(true);
        }
    }

    public void HideWinObjects()
    {
        foreach(GameObject g in winIndicators)
        {
            g.SetActive(false);
        }
    }
}

