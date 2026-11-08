using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class FloaterHolder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] List<FloaterScript> Floaters;
    [SerializeField] public float depthBefSub;
    [SerializeField] public float displacementAmt;
    [SerializeField] public int floaters;

    [SerializeField] public float waterDrag;
    [SerializeField] public float waterAngularDrag;
    [SerializeField] public float floatieness;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        foreach (FloaterScript floater in Floaters)
        {
            floater.rb = rb;
            floater.depthBefSub = depthBefSub;
            floater.displacementAmt = displacementAmt;
            floater.floaters = floaters;
            floater.waterDrag = waterDrag;
            floater.waterAngularDrag = waterAngularDrag;
            floater.floatieness = floatieness;
        }
    }

    private void OnDisable()
    {
        foreach (FloaterScript floater in Floaters)
        {
            floater.enabled = false;
        }
    }

    private void OnEnable()
    {
        foreach (FloaterScript floater in Floaters)
        {
            floater.enabled = true;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        foreach (FloaterScript floater in Floaters)
        {
            floater.rb = rb;
            floater.depthBefSub = depthBefSub;
            floater.displacementAmt = displacementAmt;
            floater.floaters = floaters;
            floater.waterDrag = waterDrag;
            floater.waterAngularDrag = waterAngularDrag;
            floater.floatieness = floatieness;
        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (isOnline)
    //    {
    //        if (collision.gameObject.GetComponent<OnlinePlayer>() == null)
    //        {
    //            return;
    //        }
    //        if (collision.gameObject.GetComponent<OnlinePlayer>().IsClient)
    //        {
    //            Debug.Log("ReEvalVeolcity");
    //            var force = collision.rigidbody.GetPointVelocity(collision.transform.position);
    //            if(force.y < 0)
    //            {
    //                collision.rigidbody.AddForce(new Vector3(0, -force.y, 0), ForceMode.Force);
    //            }
    //        }
    //    }

    //}
}
