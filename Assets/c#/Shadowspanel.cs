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
    [SerializeField] Light sun;
    UniversalAdditionalLightData urpsunsettings;
    void Awake()
    {
        urpsunsettings = sun.GetComponent<UniversalAdditionalLightData>();
        turnoffshadowsbutton.onClick.AddListener(Turnoffshadows);
        softshadowsbutton.onClick.AddListener(Softshadows);
        hardshadowsbutton.onClick.AddListener(Hardshadows);
        highshadowsbutton.onClick.AddListener(Highshadows);
        mediumshadowsbutton.onClick.AddListener(Mediumshadows);
        lowshadowsbutton.onClick.AddListener(Lowshadows);
    }
    void Highshadows()
    {
        urpsunsettings.softShadowQuality = SoftShadowQuality.High;
    }
    void Mediumshadows()
    {
        urpsunsettings.softShadowQuality = SoftShadowQuality.Medium;
    }
    void Lowshadows()
    {
        urpsunsettings.softShadowQuality = SoftShadowQuality.Low;
    }
    void Turnoffshadows()
    {
        sun.shadows = LightShadows.None;
    }
    void Softshadows()
    {
        sun.shadows = LightShadows.Soft;
    }
    void Hardshadows()
    {
        sun.shadows = LightShadows.Hard;
    }
}
