using UnityEngine;
using UnityEngine.UI;

public class Deathpanel : MonoBehaviour
{
    [SerializeField] Button restartsandboxbutton;
    [SerializeField] Button respawnbutton;
    [SerializeField] Button mainmenubutton;
    void Start()
    {
        restartsandboxbutton.onClick.AddListener(Gamemanagement.gamemanagement.ResetScene);
        respawnbutton.onClick.AddListener(MCC.mcc.Respawn);
        respawnbutton.onClick.AddListener(Gamemanagement.gamemanagement.OnHeroRespawnButton);
    }
}
