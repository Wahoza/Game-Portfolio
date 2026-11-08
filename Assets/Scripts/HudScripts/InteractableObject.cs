using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] PanelItemScript myPanel;

    GameObject player;
    bool hasPlayer = false;
    private void Start()
    {
        player = GameManager.Instance.GetPlayer();

        player.GetComponent<TryInteract>().TryInteractEvent.AddListener(TriggerInteractionEvent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Here");

            hasPlayer = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject == player)
        {
           hasPlayer = false;
        }
        
    }

    public void TriggerInteractionEvent()
    {
        if (hasPlayer)
        {
            myPanel.Show();
        }
    }
}
