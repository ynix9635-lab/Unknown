using UnityEngine;
using UnityEngine.UI;

public class Webhookpanel : MonoBehaviour
{
    [SerializeField] Button okbutton;
    [SerializeField] GameObject webhookpanel;
    void Awake()
    {
        okbutton.onClick.AddListener(Onok);
    }
    void Onok()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Gamemanagement.gamemanagement.switchactionmap("Player");
        webhookpanel.SetActive(false);
    }
}
