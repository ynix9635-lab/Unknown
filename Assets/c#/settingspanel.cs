using UnityEngine;
using UnityEngine.UI;

public class Settingspanel : MonoBehaviour
{
    [SerializeField] Button camerabutton;
    [SerializeField] GameObject camerapanel;
    private void Awake()
    {
        camerabutton.onClick.AddListener(Camerapanel);
    }
    void Camerapanel()
    {
        camerapanel.SetActive(true);
    }
}
