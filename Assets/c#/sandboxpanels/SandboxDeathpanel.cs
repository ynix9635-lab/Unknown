using UnityEngine;
using UnityEngine.UI;

public class SandboxDeathpanel : MonoBehaviour
{
    [SerializeField] Button restartsandboxbutton;
    [SerializeField] Button respawnbutton;
    [SerializeField] Button mainmenubutton;
    void Start()
    {
        restartsandboxbutton.onClick.AddListener(Gamemanagement.reference.ResetScene);
        respawnbutton.onClick.AddListener(MCC.reference.Respawn);
        respawnbutton.onClick.AddListener(Gamemanagement.reference.OnHeroRespawnButton);
        mainmenubutton.onClick.AddListener(Gamemanagement.reference.Loadmainmenu);
    }
}
