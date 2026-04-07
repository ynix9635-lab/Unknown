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
    private void Awake()
    {
        camerabutton.onClick.AddListener(Camerapanel);
        shadowsbutton.onClick.AddListener(Shadowspanel);
        fpsbutton.onClick.AddListener(Fpspanel);
    }
    void Closeallpanels()
    {
        shadowspanel.SetActive(false);
        camerapanel.SetActive(false);
        fpspanel.SetActive(false);
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
