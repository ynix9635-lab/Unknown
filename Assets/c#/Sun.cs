using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light))]
public class Sun : MonoBehaviour
{
    public static Sun reference;
    Light sun;
    UniversalAdditionalLightData urpsunsettings;
    private void Awake()
    {
        reference = this;
        sun = GetComponent<Light>();
        urpsunsettings = sun.GetComponent<UniversalAdditionalLightData>();
    }
    public void Settime(Quaternion sunrotation,float suntemperature)
    {
        sun.transform.rotation = sunrotation;
        sun.colorTemperature = suntemperature;
    }
    public void Highshadows()
    {
        urpsunsettings.softShadowQuality = SoftShadowQuality.High;
        PlayerPrefs.SetString("SoftShadowQuality", "High");
    }
    public void Mediumshadows()
    {
        urpsunsettings.softShadowQuality = SoftShadowQuality.Medium;
        PlayerPrefs.SetString("SoftShadowQuality", "Medium");
    }
    public void Lowshadows()
    {
        urpsunsettings.softShadowQuality = SoftShadowQuality.Low;
        PlayerPrefs.SetString("SoftShadowQuality", "Low");
    }
    public void Turnoffshadows()
    {
        sun.shadows = LightShadows.None;
        PlayerPrefs.SetString("LightShadows", "None");
    }
    public void Softshadows()
    {
        sun.shadows = LightShadows.Soft;
        PlayerPrefs.SetString("LightShadows", "Soft");
    }
    public void Hardshadows()
    {
        sun.shadows = LightShadows.Hard;
        PlayerPrefs.SetString("LightShadows", "Hard");
    }
}
