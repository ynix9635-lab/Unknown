using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[RequireComponent(typeof(MCC))]
public class Gamemanagement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] GameObject basic_ui;
    [SerializeField] Image jumpcdfill;
    [SerializeField] Image kickcdfill;
    [SerializeField] GameObject jump;
    [SerializeField] GameObject kick;
    [SerializeField] Button quitgamebutton;
    [SerializeField] CinemachineVirtualCamera xfreelookcamera;
    [SerializeField] CinemachineFreeLook freelookcamera;
    [SerializeField] CinemachineVirtualCamera povcamera;
    float jumpstart;
    float kickstart;
    bool isjumpcd = true;
    bool iskickcd = true;
    [SerializeField] GameObject menu;
    [SerializeField] PlayerInput playerInput;
    bool ismenuopen = false;
    public static Gamemanagement gamemanagement;
    void Awake()
    {
        gamemanagement = this;
    }
    void Start()
    {
        quitgamebutton.onClick.AddListener(Quitgame);
        playerInput.SwitchCurrentActionMap("Player");
    }
    public void Onxfreecam(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            xfreelookcamera.Priority = 1;
            freelookcamera.Priority = 0;
            povcamera.Priority = 0;
            Camerascript.camerascript.SwitchPOVmode(false);
        }
    }
    public void Onfreecam(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            xfreelookcamera.Priority = 0;
            freelookcamera.Priority = 1;
            povcamera.Priority = 0;
            Camerascript.camerascript.SwitchPOVmode(false);
        }
    }
    public void Onpovcam(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            xfreelookcamera.Priority = 0;
            freelookcamera.Priority = 0;
            povcamera.Priority = 1;
            Camerascript.camerascript.SwitchPOVmode(true);
        }
    }
    public void Quitgame()
    {
        Application.Quit();
    }
    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Openmenu();
        }
    }
    public void OnKick()
    {
        iskickcd = true;
        kickstart = Time.time;
        kick.SetActive(true);
    }
    public void OnJump()
    {
        if (!isjumpcd)
        {
            isjumpcd = true;
            jumpstart = Time.time;
            jump.SetActive(true);
        }
    }
    public void Respawn()
    {
        playerInput.SwitchCurrentActionMap("Player");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Openmenu()
    {
        ismenuopen = !ismenuopen;
        menu.SetActive(ismenuopen);
        if (ismenuopen)
        {
            playerInput.SwitchCurrentActionMap("UI");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
            playerInput.SwitchCurrentActionMap("Player");

        }
    }
    void Update()
    {
        if (iskickcd)
        {
            if(Time.time - kickstart < MCC.kickcd)
            {
                kickcdfill.fillAmount = 1 - ((Time.time - kickstart) / MCC.kickcd);
            }
            else
            {
                kickcdfill.fillAmount = 0f;
                iskickcd = false;
                kick.SetActive(false);
            }
        }
        if (isjumpcd)
        {
            if (Time.time - jumpstart < MCC.jumpcd)
            {
                jumpcdfill.fillAmount = 1 - ((Time.time - jumpstart)/MCC.jumpcd);
            }
            else
            {
                jumpcdfill.fillAmount = 0f;
                isjumpcd = false;
                jump.SetActive(false);
            }
        }
    }
}
