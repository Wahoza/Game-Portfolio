using System.Runtime.CompilerServices;
using UnityEngine;

public class PointerCreator : MonoBehaviour
{
    [SerializeField] GameObject pointerPrefab;
    [SerializeField] GameObject mapPointerPrefab;
    [SerializeField] Vector3 Offset;
    Transform hudCanvas;
    Transform mapCanvas;
    [SerializeField] string myName;
    private void Start()
    {
        hudCanvas = UIManager.Instance.PointerCanvas;

        var pointer = Instantiate(pointerPrefab, hudCanvas);
        pointer.GetComponent<PlayerPointer>().SetPlayer(transform);
        pointer.GetComponent<PlayerPointer>().SetText(myName);
        pointer.GetComponent<PlayerPointer>().SetOffset(Offset);

        mapCanvas = UIManager.Instance.MapCanvas;
        var mapPointer = Instantiate(mapPointerPrefab, mapCanvas);
        mapPointer.GetComponent<MapPointer>().SetPlayer(transform);
        mapPointer.GetComponent<MapPointer>().SetText(myName);

    }
}
