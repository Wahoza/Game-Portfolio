using UnityEngine;
using UnityEngine.Splines;

public class SplineFollow : MonoBehaviour
{
    [SerializeField] SplineContainer spline;
    [SerializeField] float speed;

    private float splineLength;
    private float distancePercentage;
    void Start()
    {
        splineLength = spline.CalculateLength();
    }

    // Update is called once per frame
    void Update()
    {
        distancePercentage += speed * Time.deltaTime / splineLength;

        Vector3 currentPos = spline.EvaluatePosition(distancePercentage);

        transform.position = currentPos;

        if (distancePercentage >= 1f)
        {
            distancePercentage = 0f;
        }

    }
}
