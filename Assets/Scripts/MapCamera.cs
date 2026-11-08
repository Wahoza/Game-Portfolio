using System.Collections;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return null;
        gameObject.SetActive(false);
    }

    public void UpdateMapCamera()
    {
        gameObject.SetActive(true);

        StartCoroutine(Start());
    }

    public void EnableMapCamera()
    {
        gameObject.SetActive(true);
    }

    public void DisableMapCamera()
    {
        gameObject.SetActive(false);
    }
}
