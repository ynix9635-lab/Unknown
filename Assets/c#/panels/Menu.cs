using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Button settingsbutton;
    [SerializeField] Button mainmenubutton;
    [SerializeField] GameObject settingspanel;
    void Start()
    {
        settingsbutton.onClick.AddListener(Settings);
        mainmenubutton.onClick.AddListener(Gamemanagement.gamemanagement.Loadmainmenu);
    }
    void Settings()
    {
        settingspanel.SetActive(true);
    }
}
