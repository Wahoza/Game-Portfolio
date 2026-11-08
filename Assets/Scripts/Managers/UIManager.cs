using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get ; private set;}

    [field : SerializeField] public Camera mainCamera { get; private set; }
    [field : SerializeField] public Camera mapCamera { get; private set; }
    [field: SerializeField] public ProjectPanelComponent ProjectInfoPanel { get; private set; }
    [field: SerializeField] public RectTransform PointerCanvas {  get; private set; }
    [field: SerializeField] public RectTransform MapCanvas { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }
}
