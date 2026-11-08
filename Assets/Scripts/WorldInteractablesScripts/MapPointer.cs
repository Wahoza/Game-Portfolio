using System.Collections;
using TMPro;
using UnityEngine;

public class MapPointer : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Camera mapCam;
    public Transform m_player;
    private RectTransform parentRect;
    RectTransform renderTextureUI;

    [SerializeField] TextMeshProUGUI myText;
    [SerializeField] bool isPlayerPointer = false;

    private Vector3 myOffset = Vector3.zero;
    void Start()
    {
        mapCam = UIManager.Instance.mapCamera;
        
        if(transform.parent != null)
        {
            parentRect = transform.parent.GetComponent<RectTransform>();
        }

        if (isPlayerPointer)
        {
            transform.SetAsLastSibling();
        }
        else
        {
            transform.SetAsFirstSibling();
        }

        if (mapCam == null || parentRect == null)
        {
            Destroy(gameObject);
            return;
        }

        StartCoroutine(UpdateRoutine());
    }

    // Update is called once per frame
    IEnumerator UpdateRoutine()
    {
        while (true)
        {

            if (m_player != null)
            {
                Vector3 screenPos = mapCam.WorldToViewportPoint(m_player.position);
                Vector3 boxBoundsRect = new Vector3(parentRect.rect.width, parentRect.rect.height, 0);
                Vector3 adjustedScreenPos = new Vector3(boxBoundsRect.x * screenPos.x, screenPos.y * boxBoundsRect.y, 0) - boxBoundsRect / 2;

                transform.localPosition = Vector3.Slerp(adjustedScreenPos, transform.localPosition, 0.3f);
            }

            yield return null;
        }
    }

    public void SetPlayer(Transform player)
    {
        m_player = player;
    }

    public void SetText(string text)
    {
        myText.text = text;
    }
    public void SetOffset(Vector3 offset)
    {
        myOffset = offset;
    }


}
