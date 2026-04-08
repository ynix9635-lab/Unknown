using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Settingsmanagement : MonoBehaviour
{
    [SerializeField] Button savebutton;
    [SerializeField] Light sun;
    UniversalAdditionalLightData urpsunsettings;
    public static Settingsmanagement settingsmanagement;
    void Awake()
    {
        urpsunsettings = sun.GetComponent<UniversalAdditionalLightData>();
        settingsmanagement = this;
    }
    void Start()
    {
        savebutton.onClick.AddListener(Savesettings);
        LoadSettings();
        savebutton.onClick.AddListener(Savesettings);
    }
    void LoadSettings()
    {
        switch (PlayerPrefs.GetString("defaultcam"))
        {
            case "Free":
                Gamemanagement.gamemanagement.Freecam();
                break;
            case "Xfree":
                Gamemanagement.gamemanagement.Xfreecam();
                break;
            case "Pov":
                Gamemanagement.gamemanagement.Povcam();
                break;
            default:
                break;
        }
        Setvsync(PlayerPrefs.GetInt("vsyncsetting"));
        SetSensitivity(PlayerPrefs.GetFloat("sensitivity"));
        switch (PlayerPrefs.GetString("SoftShadowQuality"))
        {
            case "High":
                Highshadows();
                break;
            case "Medium":
                Mediumshadows();
                break;
            case "Low":
                Lowshadows();
                break;
            default:
                break;
        }
        switch (PlayerPrefs.GetString("LightShadows"))
        {
            case "None":
                Turnoffshadows();
                break;
            case "Soft":
                Softshadows();
                break;
            case "hard":
                Hardshadows();
                break;
            default:
                break;
        }
    }
    public void Savesettings()
    {
        PlayerPrefs.Save();
    }
    public void Setvsync(int vsyncsetting)
    {
        QualitySettings.vSyncCount = vsyncsetting;
        PlayerPrefs.SetInt("vsyncsetting",vsyncsetting);
    }
    public void SetSensitivity(float value)
    {
        Povcamerasensitivity.povcamerasensitivity.Setsensitivity(value);
        Freecamerasensitivity.freecamerasensitivity.Setsensitivity(value);
        Xfreecamerasensitivity.xfreecamerasensitivity.Setsensitivity(value);
        PlayerPrefs.SetFloat("sensitivity",value);
        Debug.Log(PlayerPrefs.GetFloat("sensitivity"));
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
