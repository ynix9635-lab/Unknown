using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Camerapanel : MonoBehaviour
{
    [SerializeField] Slider sensitivityslider;
    [SerializeField] TMP_InputField sensitivityinput;
    float sensitivity;
    void Awake()
    {
        sensitivityslider.onValueChanged.AddListener(Sensitivityscrollchanged);
        sensitivityinput.onValueChanged.AddListener(Sensitivityinputchanged);
    }
    void Sensitivityscrollchanged(float value)
    {
        sensitivity = value;
        sensitivityinput.text = value.ToString();
        SetSensitivity(sensitivity);
    }
    void Sensitivityinputchanged(string value)
    {
        sensitivity = float.Parse(value);
        sensitivityslider.value = float.Parse(value);
        SetSensitivity(sensitivity);
    }
    void SetSensitivity(float value)
    {
        Povcamerasensitivity.povcamerasensitivity.Setsensitivity(value);
        Freecamerasensitivity.freecamerasensitivity.Setsensitivity(value);
        Xfreecamerasensitivity.xfreecamerasensitivity.Setsensitivity(value);
    }
}
