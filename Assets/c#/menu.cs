using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Button sandboxbutton;
    [SerializeField] GameObject sandboxpanel;
    [SerializeField] Button settingsbutton;
    [SerializeField] GameObject settingspanel;
    void Start()
    {
        sandboxbutton.onClick.AddListener(Sandbox);
        settingsbutton.onClick.AddListener(Settings);
    }
    void Closeallpanels()
    {
        sandboxpanel.SetActive(false);
        settingspanel.SetActive(false);
    }
    void Settings()
    {
        Closeallpanels();
        settingspanel.SetActive(true);
    }
    void Sandbox()
    {
        Closeallpanels();
        sandboxpanel.SetActive(true);
    }
}
