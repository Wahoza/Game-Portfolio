using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TryInteract : MonoBehaviour
{
    private bool controllsEnabled = false;
    public UnityEvent TryInteractEvent;

    public void OnInteract(InputAction.CallbackContext value)
    {
        if(controllsEnabled && value.phase == InputActionPhase.Started)
        {
            Debug.Log("Here");

            if (!GetComponent<StartInteractScript>().canInteract) { return; }
            Debug.Log("Here2");

            TryInteractEvent.Invoke();
        }
    }

    public void EnableControlls(bool enabled)
    {
        controllsEnabled = enabled;
    }

}