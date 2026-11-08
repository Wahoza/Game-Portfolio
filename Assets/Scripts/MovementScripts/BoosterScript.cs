using UnityEngine;

public class BoosterScript : MonoBehaviour
{
    [SerializeField] float boostSpeed;

    private void OnTriggerStay(Collider other)
    {
    
       
            if(other.gameObject.name == "Boat")
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * boostSpeed, ForceMode.Force);
            }
        
    }
    private void OnTriggerEnter(Collider other)
    {

        
            if (other.gameObject.name == "Boat")
            {
                AudioManager.Instance.PlaySFX("Ramp");
            }
        
    }
}
