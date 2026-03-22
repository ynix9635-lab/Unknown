using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenumanagement : MonoBehaviour
{
    [SerializeField] Button quitgamebutton;
    [SerializeField] Button sandboxbutton;
    private void Awake()
    {
        quitgamebutton.onClick.AddListener(Quitgame);
        sandboxbutton.onClick.AddListener(Loadsandbox);
    }
    void Quitgame()
    {
        Application.Quit();
    }
    void Loadsandbox()
    {
        SceneManager.LoadScene("sandbox");
    }
}
