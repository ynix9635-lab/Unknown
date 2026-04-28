using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Settingsmanagement : MonoBehaviour
{
    [SerializeField] Button savebutton;
    UniversalRenderPipelineAsset urpasset;
    public static Settingsmanagement reference;
    void Awake()
    {
        reference = this;
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
                Gamemanagement.reference.Freecam();
                break;
            case "Xfree":
                Gamemanagement.reference.Xfreecam();
                break;
            case "Pov":
                Gamemanagement.reference.Povcam();
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
                Sun.reference.Highshadows();
                break;
            case "Medium":
                Sun.reference.Mediumshadows();
                break;
            case "Low":
                Sun.reference.Lowshadows();
                break;
            default:
                break;
        }
        switch (PlayerPrefs.GetString("LightShadows"))
        {
            case "None":
                Sun.reference.Turnoffshadows();
                break;
            case "Soft":
                Sun.reference.Softshadows();
                break;
            case "hard":
                Sun.reference.Hardshadows();
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
                Sun.reference.Turnoffshadows();
                break;
            case <= 2048:
                Setmsaa(4);
                Sun.reference.Hardshadows();
                Setshadowsdistance(30f);
                break;
            default:
                Setmsaa(8);
                Sun.reference.Softshadows();
                Sun.reference.Highshadows();
                Setshadowsdistance(50f);
                break;
        }
    }
    public void Setmsaa(int value)
    {
        if (value == 1 || value == 2 || value == 4 || value == 8)
        {
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
}
