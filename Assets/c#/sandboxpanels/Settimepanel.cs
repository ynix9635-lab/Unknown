using System;
using UnityEngine;
using UnityEngine.UI;

public class Settimepanel : MonoBehaviour
{
    [SerializeField] Button daybutton;
    [SerializeField] Button noonbutton;
    [SerializeField] Button eveningbutton;
    [SerializeField] Button nightbutton;
    [SerializeField] Button midnightbutton;
    [SerializeField] GameObject settimepanel;
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
        Sun.reference.Settime(Quaternion.Euler(90f, 0f, 0f), 6000f);
        settimepanel.SetActive(false);
    }
    void Settimenoon()
    {
        Sun.reference.Settime(Quaternion.Euler(45f, 0f, 0f), 4500f);
        settimepanel.SetActive(false);
    }
    void Settimeevening()
    {
        Sun.reference.Settime(Quaternion.Euler(10f, 0f, 0f), 2000f);
        settimepanel.SetActive(false);
    }
    void Settimenight()
    {
        Sun.reference.Settime(Quaternion.Euler(-5f, 0f, 0f), 10000f);
        settimepanel.SetActive(false);
    }
    void Settimemidnight()
    {
        Sun.reference.Settime(Quaternion.Euler(-90f, 0f, 0f), 2000f);
        settimepanel.SetActive(false);
    }
}
