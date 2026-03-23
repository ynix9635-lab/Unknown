using UnityEngine;
using UnityEngine.UI;

public class Sandboxpanel : MonoBehaviour
{
    [SerializeField] Takedata takedata;
    [SerializeField] Dummy dummy;
    [SerializeField] Button antsizebutton;
    [SerializeField] Button normalsizebutton;
    [SerializeField] Button giantsizebutton;
    [SerializeField] Button respawnbutton;
    [SerializeField] Button flymodebutton;
    [SerializeField] Button killdummybutton;
    [SerializeField] Button resetdummybutton;
    [SerializeField] Button setmaxhpbutton;
    [SerializeField] Button setmaxstaminabutton;
    [SerializeField] Button settimebutton;
    [SerializeField] GameObject settimepanel;
    Vector3 scale = new(0.1f,0.1f,0.1f);

    void Start()
    {
        settimebutton.onClick.AddListener(Settime);
        respawnbutton.onClick.AddListener(Gamemanagement.gamemanagement.ResetScene);
        flymodebutton.onClick.AddListener(MCC.mcc.ToggleFlyMode);
        killdummybutton.onClick.AddListener(dummy.Die);
        resetdummybutton.onClick.AddListener(dummy.Respawn);
        antsizebutton.onClick.AddListener(Setscaleheroant);
        normalsizebutton.onClick.AddListener(Setscaleheronormal);
        giantsizebutton.onClick.AddListener(Setscaleherogiant);
        setmaxhpbutton.onClick.AddListener(Setmaxhp);
        setmaxstaminabutton.onClick.AddListener(Setmaxstamina);
    }
    void Settime()
    {
        settimepanel.SetActive(true);
    }
    void Setmaxhp()
    {
        takedata.Takedatastart(true);
    }
    void Setmaxstamina()
    {
        takedata.Takedatastart(false);
    }
    void Setscaleherogiant()
    {
        MCC.mcc.Setscale(1.3f);
    }
    void Setscaleheronormal()
    {
        MCC.mcc.Setscale(1f);
    }
    void Setscaleheroant()
    {
        MCC.mcc.Setscale(0.3f);
    }
}
