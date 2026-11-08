using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerPointer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Camera mainCamera;
    public Transform m_player;
    RectTransform renderTextureUI;

    [SerializeField] TextMeshProUGUI myText;
    private Vector3 myOffset = Vector3.zero;


    void OnEnable()
    {
        if (UIManager.Instance != null)
        {
            mainCamera = UIManager.Instance.mainCamera;
        }

        if(mainCamera == null)
        {
            mainCamera = Camera.main;
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
                Vector3 screenPos = mainCamera.WorldToScreenPoint(m_player.position);


                //    Debug.Log(m_player.position);
                //    //Debug.Log(mainCamera.WorldToScreenPoint(player.position));
                //    //Debug.Log(transform.position + "myTransform");
                //    Vector3 playerScreenPosition = mainCamera.WorldToScreenPoint(m_player.position);
                //    transform.position = playerScreenPosition;

                //    if (true)
                //    {
                transform.position = Vector3.Slerp(screenPos + myOffset, transform.position, 0.3f);
                //    }
                //}}

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
