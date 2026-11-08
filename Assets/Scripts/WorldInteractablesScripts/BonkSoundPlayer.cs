using UnityEngine;

public class BonkSoundPlayer : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name != "Water")
        {
            AudioManager.Instance.PlaySFX("Bonk");
        }
        else
        {
            AudioManager.Instance.PlaySFX("Sploosh");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Water")
        {
            Debug.Log("Splash");
            AudioManager.Instance.PlaySFX("Sploosh");
        }
    }
}
