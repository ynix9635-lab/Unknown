using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
public class Graphicspanel : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Button msaa1button;
    [SerializeField] UnityEngine.UI.Button msaa2button;
    [SerializeField] UnityEngine.UI.Button msaa4button;
    [SerializeField] UnityEngine.UI.Button msaa8button;
    [SerializeField] GameObject negnumbernotice;
    [SerializeField] GameObject formatexception;
    [SerializeField] GameObject overflowexception;
    [SerializeField] Slider shadowsrenderingdistanceslider;
    [SerializeField] TMP_Text shadowsrenderingdistanceamount;
    [SerializeField] TMP_InputField renderscaleinputfield;
    void Start()
    {
        msaa1button.onClick.AddListener(Msaa1);
        msaa2button.onClick.AddListener(Msaa2);
        msaa4button.onClick.AddListener(Msaa4);
        msaa8button.onClick.AddListener(Msaa8);
        shadowsrenderingdistanceamount.text = Convert.ToString(PlayerPrefs.GetFloat("shadowsdistance"));
        shadowsrenderingdistanceslider.value = PlayerPrefs.GetFloat("shadowsdistance")/100;
        shadowsrenderingdistanceslider.onValueChanged.AddListener(Changeshadowsrenderingdistance);
        renderscaleinputfield.onValueChanged.AddListener(ChangeRenderScale);
    }
    void ChangeRenderScale(string input)
    {
        negnumbernotice.SetActive(false);
        formatexception.SetActive(false);
        overflowexception.SetActive(false);
        try
        {
            float value = float.Parse(input);
            if (value < 0f)
            {
                negnumbernotice.SetActive(true);
            }
            else
            {
                Settingsmanagement.reference.SetRenderscale(value);
            }
        }
        catch (FormatException)
        {
            formatexception.SetActive(true);

        }
        catch (OverflowException)
        {
            overflowexception.SetActive(true);
        }
    }
    void Changeshadowsrenderingdistance(float value)
    {
        value = value * 100;
        shadowsrenderingdistanceamount.text = Convert.ToString(value);
        Settingsmanagement.reference.Setshadowsdistance(value);
    }
    void Msaa1()
    {
        Settingsmanagement.reference.Setmsaa(1);
    }
    void Msaa2()
    {
        Settingsmanagement.reference.Setmsaa(2);
    }
    void Msaa4()
    {
        Settingsmanagement.reference.Setmsaa(4);
    }
    void Msaa8()
    {
        Settingsmanagement.reference.Setmsaa(8);
    }
}
