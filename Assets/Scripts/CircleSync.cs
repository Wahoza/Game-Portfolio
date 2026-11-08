using System.Collections.Generic;
using UnityEngine;

public class CircleSync : MonoBehaviour
{
    public int PosID = Shader.PropertyToID("_Player0Position");
    public int SizeID = Shader.PropertyToID("_SizeP0");


    public List<Material> WallMaterials;
    public Camera MainCamera;
    public LayerMask Mask;
    public float circleSize;
    public float initalCircleSize = 1;
    private void Start()
    {
        circleSize = initalCircleSize;
        MainCamera = Camera.main;
        PosID = Shader.PropertyToID("_Player0Position");
        SizeID = Shader.PropertyToID("_SizeP0");

        SetCircleSize(circleSize);
    }
    void Update()
    {
        if(MainCamera == null) {return;}

        Vector3 dir = MainCamera.transform.position - transform.position;
        Ray ray = new Ray(transform.position, dir.normalized);
        float length = (MainCamera.transform.position - transform.position).magnitude;
        if (Physics.Raycast(ray, length, Mask))
        {
            foreach (Material mat in WallMaterials)
            {
                mat.SetFloat(SizeID, circleSize);

                //Vector3 view = MainCamera.WorldToViewportPoint(transform.position);
                mat.SetVector(PosID, transform.position);
            }
        }
        else
        {
            foreach (Material mat in WallMaterials)
            {
                mat.SetFloat(SizeID, 0);
            }
        }
    }
    public void SetCamera(Camera _camera)
    {
        MainCamera = _camera;
    }
    public void SetCircleSize(float newSize)
    {
        circleSize = newSize;

        foreach (Material mat in WallMaterials)
        {

            mat.SetFloat(SizeID, circleSize);
        }

    }
    public void ResetCircleSize()
    {
        circleSize = initalCircleSize;

        foreach (Material mat in WallMaterials)
        {

            mat.SetFloat(SizeID, circleSize);
        }
    }
    private void OnDisable()
    {
        circleSize = 0;

        foreach (Material mat in WallMaterials)
        {

            mat.SetFloat(SizeID, circleSize);
        }
    }
}
