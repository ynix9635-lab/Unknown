using UnityEngine;
using UnityEngine.UI;

public class Sandboxpanel : MonoBehaviour
{
    [SerializeField] Gamemanagement gamemanager;
    [SerializeField] MCC mcc;
    [SerializeField] Dummy dummy;
    [SerializeField] Button respawnbutton;
    [SerializeField] Button flymodebutton;
    [SerializeField] Button killdummybutton;
    [SerializeField] Button resetdummybutton;

    void Start()
    {
        respawnbutton.onClick.AddListener(Respawn);
        flymodebutton.onClick.AddListener(ToggleFlymode);
        killdummybutton.onClick.AddListener(dummy.Die);
        resetdummybutton.onClick.AddListener(dummy.Respawn);

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
