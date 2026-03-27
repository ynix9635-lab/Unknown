using UnityEngine;
using UnityEngine.UI;
public class Deathpanel : MonoBehaviour
{
    [SerializeField] Button restartlevelbutton;
    [SerializeField] Button mainmenubutton;
    void Start()
    {
        restartlevelbutton.onClick.AddListener(Gamemanagement.gamemanagement.ResetScene);
        mainmenubutton.onClick.AddListener(Gamemanagement.gamemanagement.Loadmainmenu);
    }
}
