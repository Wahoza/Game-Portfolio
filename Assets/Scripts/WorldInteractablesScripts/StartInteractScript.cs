using UnityEngine;

public class StartInteractScript : MonoBehaviour
{
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] InteractAppearScript interactAppearScript;

    public bool canInteract { get; private set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        canInteract = true;

        var isInMask = interactionLayer == (interactionLayer | (1 << other.gameObject.layer));
        if (isInMask)
        {
            interactAppearScript.AppearStart();
        }
    }    
    private void OnTriggerExit(Collider other)
    {
        canInteract = false;

        var isInMask = interactionLayer == (interactionLayer | (1 << other.gameObject.layer));
        if (isInMask)
        {
            interactAppearScript.Hide();
        }
    }
}
