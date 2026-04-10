using System;
using UnityEngine;
using UnityEngine.UI;

public class Settimepanel : MonoBehaviour
{
    Quaternion sunrotation;
    [SerializeField] Button daybutton;
    [SerializeField] Button noonbutton;
    [SerializeField] Button eveningbutton;
    [SerializeField] Button nightbutton;
    [SerializeField] Button midnightbutton;
    [SerializeField] GameObject settimepanel;
    [SerializeField] Light sun;
    private void Start()
    {
        daybutton.onClick.AddListener(Settimeday);
        noonbutton.onClick.AddListener(Settimenoon);
        eveningbutton.onClick.AddListener(Settimeevening);
        nightbutton.onClick.AddListener(Settimenight);
        midnightbutton.onClick.AddListener(Settimemidnight);
    }
    void Settimeday()
    {
        sunrotation = Quaternion.Euler(90f, 0f, 0f);
        sun.transform.rotation = sunrotation;
        sun.colorTemperature = 6000f;
        settimepanel.SetActive(false);
    }
    void Settimenoon()
    {
        sunrotation = Quaternion.Euler(45f, 0f, 0f);
        sun.transform.rotation = sunrotation;
        sun.colorTemperature = 4500f;
        settimepanel.SetActive(false);
    }
    void Settimeevening()
    {
        sunrotation = Quaternion.Euler(10f, 0f, 0f);
        sun.transform.rotation = sunrotation;
        sun.colorTemperature = 2000f;
        settimepanel.SetActive(false);
    }
    void Settimenight()
    {
        sunrotation = Quaternion.Euler(-5f, 0f, 0f);
        sun.transform.rotation = sunrotation;
        sun.colorTemperature = 10000f;
        settimepanel.SetActive(false);
    }
    void Settimemidnight()
    {
        sunrotation = Quaternion.Euler(-90f, 0f, 0f);
        sun.transform.rotation = sunrotation;
        settimepanel.SetActive(false);
    }
}
