using System.Collections;
using UnityEngine;

public class ParticleSystemPause : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return null;

        GetComponent<ParticleSystem>().Pause();
    }
}
