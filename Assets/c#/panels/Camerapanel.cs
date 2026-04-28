using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class Camerapanel : MonoBehaviour
{
    [SerializeField] Slider sensitivityslider;
    [SerializeField] TMP_InputField sensitivityinput;
    float sensitivity;
    void Start()
    {
        sensitivityslider.value = PlayerPrefs.GetFloat("sensitivity");
        sensitivityinput.text = Convert.ToString(PlayerPrefs.GetFloat("sensitivity"));
        sensitivityslider.onValueChanged.AddListener(Sensitivityscrollchanged);
        sensitivityinput.onValueChanged.AddListener(Sensitivityinputchanged);
    }
    void Sensitivityscrollchanged(float value)
    {
        sensitivity = value;
        sensitivityinput.text = value.ToString();
        Settingsmanagement.reference.SetSensitivity(sensitivity);
    }
    void Sensitivityinputchanged(string value)
    {
        sensitivity = float.Parse(value);
        sensitivityslider.value = float.Parse(value);
        Settingsmanagement.reference.SetSensitivity(sensitivity);
    }
}
