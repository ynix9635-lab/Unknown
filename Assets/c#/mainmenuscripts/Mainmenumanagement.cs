using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenumanagement : MonoBehaviour
{
    [SerializeField] Button quitgamebutton;
    [SerializeField] Button sandboxbutton;
    [SerializeField] Button levelsbutton;
    [SerializeField] GameObject levelspanel;
    [SerializeField] Button settingsbutton;
    [SerializeField] GameObject settingspanel;
    private void Start()
    {
        quitgamebutton.onClick.AddListener(Quitgame);
        sandboxbutton.onClick.AddListener(Loadsandbox);
        levelsbutton.onClick.AddListener(Levels);
        settingsbutton.onClick.AddListener(Settingspanel);
    }
    void Quitgame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
    void Loadsandbox()
    {
        SceneManager.LoadScene("sandbox");
    }
    void Settingspanel()
    {
        Closeallpanels();
        settingspanel.SetActive(true);
    }
    void Levels()
    {
        Closeallpanels();
        levelspanel.SetActive(true);
    }
    void Closeallpanels()
    {
        levelspanel.SetActive(false);
        settingspanel.SetActive(false);
    }
}
