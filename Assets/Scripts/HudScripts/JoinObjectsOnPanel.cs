using UnityEngine;

public class JoinObjectsOnPanel : MonoBehaviour
{
    [SerializeField] public int importance;
    private void Start()
    {
        UIManager.Instance.ProjectInfoPanel.Join(this);
    }
}
