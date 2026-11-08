using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ProjectPanelComponent : MonoBehaviour
{
    [SerializeField] RectTransform contentHolder;
    private List<JoinObjectsOnPanel> panelObjects;

    private bool willUpdateNextFrame = false;
    public void Join(JoinObjectsOnPanel obj)
    {
        if(panelObjects == null)
        {
            panelObjects = new();
        }
        panelObjects.Add(obj);

        obj.transform.SetParent(contentHolder);
        obj.transform.eulerAngles = Vector3.zero;
        obj.transform.localScale = Vector3.one;

        if (!willUpdateNextFrame)
        {
            willUpdateNextFrame = true;
            UIManager.Instance.StartCoroutine(UpdateNextFrame());
        }
    }

    private IEnumerator UpdateNextFrame()
    {
        yield return null;


        foreach (var panel in panelObjects.OrderBy(panel => panel.importance))
        {
            panel.transform.SetAsLastSibling();
        }

        willUpdateNextFrame = false;

        yield return null;
    }
}
