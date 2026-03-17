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
        antmodebutton.onClick.AddListener(setscaleheroant);
        giantmodebutton.onClick.AddListener(setscaleherogiant);
    }
    void setscaleherogiant()
    {
        scale.x = 1.3f;
        scale.z = 1.3f;
        scale.y = 1.3f;
        MCC.mcc.Setscale(scale);
    }
    void setscaleheroant()
    {
        scale.x = 0.1f;
        scale.z = 0.1f;
        scale.y = 0.1f;
        MCC.mcc.Setscale(scale);
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
