using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fpspanel : MonoBehaviour
{
    [SerializeField] Button vsynconbutton;
    [SerializeField] Button vsyncoffbutton;
    private void Awake()
    {
        vsynconbutton.onClick.AddListener(Vsyncon);
        vsyncoffbutton.onClick.AddListener(Vsyncoff);
    }
    void Vsyncon()
    {
        Settingsmanagement.settingsmanagement.Setvsync(1);
        Debug.Log("on vsync");
    }
    void Vsyncoff()
    {
        Settingsmanagement.settingsmanagement.Setvsync(0);
        Debug.Log("off vsync");
    }
}
