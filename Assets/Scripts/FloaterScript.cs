using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloaterScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody rb;
    public float depthBefSub;
    public float displacementAmt;
    public int floaters;

    public float waterDrag;
    public float waterAngularDrag;
    public float floatieness;

    //public List<GameObject> waterList;
    //private int waterCounts;
    Collider[] colliders;
    void Start()
    {
        StartCoroutine(UpdateRoutine());   
    }

    IEnumerator UpdateRoutine()
    {
        while (true)
        {
            DoFloaterPhysics();
            yield return new WaitForFixedUpdate();
        }
    }

    // Update is called once per frame
    void DoFloaterPhysics()
    {
        //if on water
        //if(waterCounts < 0) {return;}
        colliders = Physics.OverlapSphere(transform.position, 0.1f, LayerMask.GetMask("Water"), QueryTriggerInteraction.Collide);
        foreach(Collider collider in colliders)
        {
            rb.AddForceAtPosition(Physics.gravity / floaters, transform.position, ForceMode.Acceleration);

            //BoxCollider waterCollider = hit.collider.gameObject.GetComponent<BoxCollider>();

            //if (waterCollider == null) { return; }

            float projectedPosY = collider.bounds.max.y;

            if (transform.position.y < projectedPosY)
            {
                float displacementMulti = Mathf.Clamp01((projectedPosY - transform.position.y) / depthBefSub) * displacementAmt;

                rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMulti, 0f), transform.position, ForceMode.Acceleration);
                rb.AddForce(displacementMulti * -rb.linearVelocity * waterDrag * Time.fixedDeltaTime * floatieness, ForceMode.VelocityChange);
                rb.AddTorque(displacementMulti * -rb.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            }
        }
        //if (Physics.Raycast(transform.position, Vector3.down, out hit, 10, LayerMask.GetMask("Water"), QueryTriggerInteraction.Collide))
        //{

        //}
    }
    //public void EnterWater(GameObject other)
    //{
    //    if(other != null)
    //    {
    //        waterCounts++;
    //        if (!waterList.Contains(other)) {
    //            waterList.Add(other);    
    //        }
    //    }
    //}    
    //public void ExitWater(GameObject other)
    //{
    //    if (other != null)
    //    {
    //        waterCounts--;
    //        if (waterList.Contains(other))
    //        {
    //            waterList.Remove(other);
    //        }
    //    }
    //}
}
