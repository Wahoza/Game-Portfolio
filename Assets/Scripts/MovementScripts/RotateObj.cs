using UnityEngine;

public class RotateObj : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 rotationAxes;

    private void Update()
    {
        transform.Rotate(rotationAxes, rotationSpeed * Time.deltaTime);
    }

}
