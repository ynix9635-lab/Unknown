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
        Settingsmanagement.settingsmanagement.SetSensitivity(sensitivity);
    }
    void Sensitivityinputchanged(string value)
    {
        sensitivity = float.Parse(value);
        sensitivityslider.value = float.Parse(value);
        Settingsmanagement.settingsmanagement.SetSensitivity(sensitivity);
    }
}
