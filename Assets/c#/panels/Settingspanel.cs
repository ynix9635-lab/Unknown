using UnityEngine;
using UnityEngine.UI;

public class Settingspanel : MonoBehaviour
{
    [SerializeField] Button camerabutton;
    [SerializeField] GameObject camerapanel;
    [SerializeField] Button shadowsbutton;
    [SerializeField] GameObject shadowspanel;
    private void Awake()
    {
        camerabutton.onClick.AddListener(Camerapanel);
        shadowsbutton.onClick.AddListener(Shadowspanel);
    }
    void Camerapanel()
    {
        camerapanel.SetActive(true);
        shadowspanel.SetActive(false);
    }
    void Shadowspanel()
    {
        shadowspanel.SetActive(true);
        camerapanel.SetActive(false);
    }
}
