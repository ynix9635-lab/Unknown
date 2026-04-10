using UnityEngine;
using UnityEngine.UI;

public class Webhookpanel : MonoBehaviour
{
    [SerializeField] Button okbutton;
    [SerializeField] GameObject webhookpanel;
    void Start()
    {
        okbutton.onClick.AddListener(Onok);
    }
    void Onok()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Gamemanagement.gamemanagement.Switchactionmap("Player");
        webhookpanel.SetActive(false);
    }
}
