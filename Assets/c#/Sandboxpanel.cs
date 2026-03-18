using UnityEngine;
using UnityEngine.UI;

public class Sandboxpanel : MonoBehaviour
{
    [SerializeField] Gamemanagement gamemanager;
    [SerializeField] MCC mcc;
    [SerializeField] Dummy dummy;
    [SerializeField] Button antmodebutton;
    [SerializeField] Button giantmodebutton;
    [SerializeField] Button respawnbutton;
    [SerializeField] Button flymodebutton;
    [SerializeField] Button killdummybutton;
    [SerializeField] Button resetdummybutton;
    Vector3 scale = new(0.1f,0.1f,0.1f);

    void Start()
    {
        respawnbutton.onClick.AddListener(Respawn);
        flymodebutton.onClick.AddListener(ToggleFlymode);
        killdummybutton.onClick.AddListener(dummy.Die);
        resetdummybutton.onClick.AddListener(dummy.Respawn);
        antmodebutton.onClick.AddListener(Setscaleheroant);
        giantmodebutton.onClick.AddListener(Setscaleherogiant);
    }
    void Setscaleherogiant()
    {
        MCC.mcc.Setscale(1.3f);
    }
    void Setscaleheroant()
    {
        MCC.mcc.Setscale(0.3f);
    }
    void Respawn()
    { 
        gamemanager.Respawn();
    }
    void ToggleFlymode()
    {
        mcc.ToggleFlyMode();
    }
}
