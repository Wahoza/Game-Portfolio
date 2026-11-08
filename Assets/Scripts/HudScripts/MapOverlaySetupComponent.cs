using UnityEngine;
using UnityEngine.UI;

public class MapOverlaySetupComponent : MonoBehaviour
{
    [SerializeField] string UICamTag;
    Camera m_Camera;
    Canvas m_Canvas;
    CanvasScaler m_Scaler;
    private void Start()
    {
        m_Camera = GameObject.FindGameObjectWithTag(UICamTag)?.GetComponent<Camera>();
        m_Canvas = GetComponent<Canvas>();
        m_Scaler = GetComponent<CanvasScaler>();

        if( m_Scaler == null || m_Canvas == null || m_Camera == null)
            return;

        m_Scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
        m_Canvas.renderMode = RenderMode.ScreenSpaceCamera;
        m_Canvas.worldCamera = m_Camera;
    }
}
