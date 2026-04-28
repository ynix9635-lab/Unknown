using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fpspanel : MonoBehaviour
{
    [SerializeField] Button vsynconbutton;
    [SerializeField] Button vsyncoffbutton;
    private void Start()
    {
        vsynconbutton.onClick.AddListener(Vsyncon);
        vsyncoffbutton.onClick.AddListener(Vsyncoff);
    }
    void Vsyncon()
    {
        Settingsmanagement.reference.Setvsync(1);
        Debug.Log("on vsync");
    }
    void Vsyncoff()
    {
        Settingsmanagement.reference.Setvsync(0);
        Debug.Log("off vsync");
    }
}
