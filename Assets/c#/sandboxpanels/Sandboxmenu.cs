using System;
using UnityEngine;
using UnityEngine.UI;

public class Sandboxmenu : MonoBehaviour
{
    [SerializeField] Button sandboxbutton;
    [SerializeField] GameObject sandboxpanel;
    [SerializeField] Button settingsbutton;
    [SerializeField] GameObject settingspanel;
    [SerializeField] GameObject bugreportpanel;
    [SerializeField] GameObject webhookpanel;
    [SerializeField] Button mainmenubutton;
    void Start()
    {
        mainmenubutton.onClick.AddListener(Gamemanagement.gamemanagement.Loadmainmenu);
        sandboxbutton.onClick.AddListener(Sandbox);
        settingsbutton.onClick.AddListener(Settings);
    }
    private void OnEnable()
    {
        bugreportpanel.SetActive(false);
        webhookpanel.SetActive(false);
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
