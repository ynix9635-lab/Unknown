using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class Settingspanel : MonoBehaviour
{
    [SerializeField] Button camerabutton;
    [SerializeField] GameObject camerapanel;
    [SerializeField] Button shadowsbutton;
    [SerializeField] GameObject shadowspanel;
    [SerializeField] Button fpsbutton;
    [SerializeField] GameObject fpspanel;
    [SerializeField] Button graphicsbutton;
    [SerializeField] GameObject graphicspanel;
    [SerializeField] Button recommendedbutton;
    void Start()
    {
        camerabutton.onClick.AddListener(Camerapanel);
        shadowsbutton.onClick.AddListener(Shadowspanel);
        fpsbutton.onClick.AddListener(Fpspanel);
        graphicsbutton.onClick.AddListener(Graphics);
        recommendedbutton.onClick.AddListener(Recommended);
    }
    void Recommended()
    {
        Settingsmanagement.settingsmanagement.SetRecommendedSettings();
    }
    void Closeallpanels()
    {
        graphicspanel.SetActive(false);
        shadowspanel.SetActive(false);
        camerapanel.SetActive(false);
        fpspanel.SetActive(false);
    }
    void Graphics()
    {
        Closeallpanels();
        graphicspanel.SetActive(true);
    }
    void Fpspanel()
    {
        Closeallpanels();
        fpspanel.SetActive(true);
    }
    void Camerapanel()
    {
        Closeallpanels();
        camerapanel.SetActive(true);
    }
    void Shadowspanel()
    {
        Closeallpanels();
        shadowspanel.SetActive(true);
    }
}
