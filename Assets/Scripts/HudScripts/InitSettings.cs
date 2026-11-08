using UnityEngine;

public class InitSettings : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] SettingsScript settings;
    void Start()
    {
        settings.InitSettings();
    }
}
