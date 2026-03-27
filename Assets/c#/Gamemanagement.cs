using Cinemachine;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[RequireComponent(typeof(MCC))]
[RequireComponent(typeof(PlayerInput))]
public class Gamemanagement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] TMP_Text fps;
    [SerializeField] CinemachineVirtualCamera xfreelookcamera;
    [SerializeField] CinemachineFreeLook freelookcamera;
    [SerializeField] CinemachineVirtualCamera povcamera;
    [SerializeField] GameObject menu;
    PlayerInput playerInput;
    [SerializeField] GameObject deathpanel;
    bool ismenuopen = false;
    float lastfpsshow;
    int fpscount = 0;
    public static Gamemanagement gamemanagement;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        gamemanagement = this;
    }
    void Start()
    {
        playerInput.SwitchCurrentActionMap("Player");
    }
    public void Loadmainmenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("mainmenu");
    }
    public void Switchactionmap(string actionmap)
    {
        playerInput.SwitchCurrentActionMap(actionmap);
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
    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Openmenu();
        }
    }
    public void ResetScene()
    {
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
    public void OnHeroDeath()
    {
        deathpanel.SetActive(true);
        playerInput.SwitchCurrentActionMap("UI");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
    public void OnHeroRespawnButton()
    {
        deathpanel.SetActive(false);
    }
    private void Update()
    {
        fpscount++;
        if (Time.time - lastfpsshow > 1f)
        {
            fps.text = Convert.ToString(fpscount);
            fpscount = 0;
            lastfpsshow = Time.time;
        }
    }
}
