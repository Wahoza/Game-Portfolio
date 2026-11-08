using DG.Tweening;
using UnityEngine;

public class SuckScript : MonoBehaviour
{
    [SerializeField] GameObject myParticleSystem;
    [SerializeField] Transform goalPosition;
    [SerializeField] Transform evilDomainSpawnPosition;
    [SerializeField] float animDuration;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<BoatMover>();
        if (other.GetComponent<BoatMover>() != null)
        {
            player.EnableControlls(false);
            player.GetComponent<Rigidbody>().isKinematic = true;
            player.transform.DOMove(goalPosition.position, animDuration).OnComplete(() => FinishAnim(player));

            myParticleSystem.SetActive(true);
            myParticleSystem.transform.LookAt(player.transform.position);

            AudioManager.Instance.PlaySFX("Suck");
            AudioManager.Instance.PauseBoatSounds();
        }
    }
    private void FinishAnim(BoatMover player)
    {
        player.GetComponent<Rigidbody>().isKinematic = false;

        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        player.EnableControlls(true);

        player.transform.position = evilDomainSpawnPosition.position;

        myParticleSystem.SetActive(false);   
    }
}
