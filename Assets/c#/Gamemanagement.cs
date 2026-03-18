using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(MCC))]
public class Gamemanagement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] GameObject basic_ui;
    [SerializeField] Image jumpcdfill;
    [SerializeField] Button quitgamebutton;
    float jumpstart;
    bool isjumpcd = true;
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
    public void OnJump()
    {
        if (!isjumpcd)
        {
            basic_ui.SetActive(true);
            isjumpcd = true;
            jumpstart = Time.time;
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
                basic_ui.SetActive(false);
            }
        }
    }
}
