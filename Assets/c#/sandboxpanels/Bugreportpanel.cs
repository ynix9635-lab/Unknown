using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Bugreportpanel : MonoBehaviour
{
    [SerializeField] GameObject bugreportpanel;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button applybutton;
    void Start()
    {
        applybutton.onClick.AddListener(Onapply);
    }
    void Onapply()
    {
        Bugreport.bugreport.SendBugReport(inputField.text);
        Time.timeScale = 1f;
        Gamemanagement.reference.Switchactionmap("Player");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        bugreportpanel.SetActive(false);
    }
}
