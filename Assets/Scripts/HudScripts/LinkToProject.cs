using UnityEngine;

public class LinkToProject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] string projectLink;
    
    public void OpenProjectLink()
    {
        Application.OpenURL(projectLink);
    }    
    public void OpenProjectLinkFromURL(string URL)
    {
        Application.OpenURL(URL);
    }
}
