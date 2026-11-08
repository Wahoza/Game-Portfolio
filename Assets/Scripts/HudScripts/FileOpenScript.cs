using UnityEngine;

public class FileOpenScript : MonoBehaviour
{
    [SerializeField] string myFileName;
    public void OpenFile()
    {
        Application.OpenURL(Application.streamingAssetsPath + $"/{myFileName}");
    }
}
