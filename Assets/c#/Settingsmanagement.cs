using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Settingsmanagement : MonoBehaviour
{
    [SerializeField] Button savebutton;
    [SerializeField] Light sun;
    UniversalRenderPipelineAsset urpasset;
    UniversalAdditionalLightData urpsunsettings;
    public static Settingsmanagement settingsmanagement;
    void Awake()
    {
        urpsunsettings = sun.GetComponent<UniversalAdditionalLightData>();
        settingsmanagement = this;
    }
    void Start()
    {
        urpasset = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
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
        SetRenderscale(PlayerPrefs.GetFloat("renderscale",1f));
        Setmsaa(PlayerPrefs.GetInt("msaa",1));
        Setshadowsdistance(PlayerPrefs.GetFloat("shadowsdistance"));
        Setvsync(PlayerPrefs.GetInt("vsyncsetting"));
        SetSensitivity(PlayerPrefs.GetFloat("sensitivity",0.5f));
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
    public void SetRecommendedSettings()
    {
        SetRenderscale(1f);
        Setvsync(1);
        switch (SystemInfo.graphicsMemorySize)
        {
            case <= 1024:
                SetRenderscale(0.9f);
                Setmsaa(2);
                Turnoffshadows();
                break;
            case <= 2048:
                Setmsaa(4);
                Hardshadows();
                Setshadowsdistance(30f);
                break;
            default:
                Setmsaa(8);
                Softshadows();
                Highshadows();
                Setshadowsdistance(50f);
                break;
        }
    }
    public void Setmsaa(int value)
    {
        if (value == 1 || value == 2 || value == 4 || value == 8)
        {
            Debug.Log(value);
            urpasset.msaaSampleCount = value;
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
    public void SetRenderscale(float value)
    {
        if(value > 0)
        {
            urpasset.renderScale = value;
            PlayerPrefs.SetFloat("renderscale", value);
        }
    }
    public void Setshadowsdistance(float value)
    {
        urpasset.shadowDistance = value;
        PlayerPrefs.SetFloat("shadowsdistance",value);
    }
    public void SetSensitivity(float value)
    {
        Povcamerasensitivity.povcamerasensitivity.Setsensitivity(value);
        Freecamerasensitivity.freecamerasensitivity.Setsensitivity(value);
        Xfreecamerasensitivity.xfreecamerasensitivity.Setsensitivity(value);
        PlayerPrefs.SetFloat("sensitivity",value);
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
