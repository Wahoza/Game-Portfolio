using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AdjustButtonImage : MonoBehaviour
{
    [SerializeField] Vector2 canvasSize = new Vector2(1920, 1080);
    private void OnEnable()
    {
        StartCoroutine(RefreshImage());
    }
    IEnumerator RefreshImage()
    {
        yield return null;

            var rawImage = GetComponent<RawImage>();

        if (rawImage == null)
            yield break;

        var tex = rawImage.texture;

        if(tex == null)
            yield break;

        var childGo = new GameObject();
        childGo.transform.parent = transform;

        childGo.name = "imageHolder";

        var rectTransform = GetComponent<RectTransform>();

        var rectSizeDelta = rectTransform.sizeDelta;


        var ratioX = rectTransform.sizeDelta.x / tex.width;
        var ratioY = rectTransform.sizeDelta.y / tex.height;

        var ratioToUse = ratioX < ratioY ? ratioX : ratioY;

        Debug.Log($"Ratio {ratioToUse}, x: {ratioX}, y: {ratioY}");
        Debug.Log($"SizeDelta, {rectSizeDelta}");

        var screenRatioX = Screen.width / canvasSize.x;
        var screenRatioY = Screen.height / canvasSize.y;

        var screenRatioToUse = screenRatioX < screenRatioY ? screenRatioX : screenRatioY;


        childGo.AddComponent<RawImage>();

        var childRect = childGo.GetComponent<RectTransform>();

        childRect.localPosition = Vector3.zero;
        childRect.localEulerAngles = Vector3.zero;
        childRect.sizeDelta = new Vector2(ratioToUse * tex.width * screenRatioToUse, ratioToUse * tex.height * screenRatioToUse);
        childGo.GetComponent<RawImage>().texture = tex;

        Destroy(rawImage);
    }


}
