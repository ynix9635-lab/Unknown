using System;
using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    [SerializeField] Button sandboxbutton;
    [SerializeField] GameObject sandboxpanel;
    void Start()
    {
        sandboxbutton.onClick.AddListener(Sandbox);
    }
    void Sandbox()
    {
        sandboxpanel.SetActive(true);
    }
}
