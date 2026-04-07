using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Shadowspanel : MonoBehaviour
{
    [SerializeField] Button turnoffshadowsbutton;
    [SerializeField] Button softshadowsbutton;
    [SerializeField] Button hardshadowsbutton;
    [SerializeField] Button highshadowsbutton;
    [SerializeField] Button mediumshadowsbutton;
    [SerializeField] Button lowshadowsbutton;
    void Awake()
    {
        turnoffshadowsbutton.onClick.AddListener(Settingsmanagement.settingsmanagement.Turnoffshadows);
        softshadowsbutton.onClick.AddListener(Settingsmanagement.settingsmanagement.Softshadows);
        hardshadowsbutton.onClick.AddListener(Settingsmanagement.settingsmanagement.Hardshadows);
        highshadowsbutton.onClick.AddListener(Settingsmanagement.settingsmanagement.Highshadows);
        mediumshadowsbutton.onClick.AddListener(Settingsmanagement.settingsmanagement.Mediumshadows);
        lowshadowsbutton.onClick.AddListener(Settingsmanagement.settingsmanagement.Lowshadows);
    }
}
