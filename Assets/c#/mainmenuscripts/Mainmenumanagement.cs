using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenumanagement : MonoBehaviour
{
    [SerializeField] Button quitgamebutton;
    [SerializeField] Button sandboxbutton;
    [SerializeField] Button levelsbutton;
    [SerializeField] GameObject levelspanel;
    private void Awake()
    {
        quitgamebutton.onClick.AddListener(Quitgame);
        sandboxbutton.onClick.AddListener(Loadsandbox);
        levelsbutton.onClick.AddListener(Levels);
    }
    void Quitgame()
    {
        Application.Quit();
    }
    void Loadsandbox()
    {
        SceneManager.LoadScene("sandbox");
    }
    void Levels()
    {
        levelspanel.SetActive(true);
    }
}
