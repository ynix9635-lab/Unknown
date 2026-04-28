using UnityEngine;
using UnityEngine.UI;
public class Deathpanel : MonoBehaviour
{
    [SerializeField] Button restartlevelbutton;
    [SerializeField] Button mainmenubutton;
    void Start()
    {
        restartlevelbutton.onClick.AddListener(Gamemanagement.reference.ResetScene);
        mainmenubutton.onClick.AddListener(Gamemanagement.reference.Loadmainmenu);
    }
}
