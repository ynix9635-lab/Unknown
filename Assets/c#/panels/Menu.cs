 using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Button sandboxbutton;
    [SerializeField] GameObject sandboxpanel;
    [SerializeField] Button equipmentbutton;
    [SerializeField] GameObject equipmentpanel;
    [SerializeField] Button settingsbutton;
    [SerializeField] GameObject settingspanel;
    [SerializeField] GameObject bugreportpanel;
    [SerializeField] GameObject webhookpanel;
    [SerializeField] Button mainmenubutton;
    void Start()
    {
        equipmentbutton.onClick.AddListener(Equipment);
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
        equipmentpanel.SetActive(false);
    }
    void Equipment()
    {
        Closeallpanels();
        equipmentpanel.SetActive(true);
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
