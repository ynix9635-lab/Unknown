using Cinemachine;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[RequireComponent(typeof(MCC))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]
public class Gamemanagement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] TMP_Text fps;
    [SerializeField] CinemachineVirtualCamera xfreelookcamera;
    [SerializeField] CinemachineFreeLook freelookcamera;
    [SerializeField] CinemachineVirtualCamera povcamera;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject deathpanel;
    PlayerInput playerInput;
    Animator animator;
    Dictionary<Dropweapon, float> distances = new();
    static readonly int IsdyingHash = Animator.StringToHash("isdying");
    bool ismenuopen = false;
    bool isweaponnear = false;
    bool istheredropweapon = true;
    float lastfpsshow;
    const float weapongrabdistance = 1f;
    int fpscount = 0;
    public static Gamemanagement reference;
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        reference = this;
    }
    void Start()
    {
        playerInput.SwitchCurrentActionMap("Player"); 
        Dropweapon[] dropweapons = FindObjectsByType<Dropweapon>(FindObjectsSortMode.None);
        foreach (var dropweapon in dropweapons)
        {
            distances.Add(dropweapon, Vector3.Distance(transform.position,dropweapon.transform.position));
        }
    }
    public void Changedistance(Dropweapon dropweapon, float distance)
    {
        distances[dropweapon] = distance;
    }
    public void OnEquip()
    {
        if (distances.Count > 0)
        {
            KeyValuePair<Dropweapon, float> pair = distances.First();
            foreach (var item in distances)
            {
                if (item.Value < pair.Value)
                {
                    pair = item;
                }
            }
            if (pair.Value < weapongrabdistance)
            {
                pair.Key.Take();
                pair.Key.gameObject.SetActive(false);
                distances.Remove(pair.Key);
            }
        }
        isweaponnear = false;
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
    public void OFFcinemachine()
    {
        xfreelookcamera.Priority = 0;
        freelookcamera.Priority = 0;
        povcamera.Priority = 0;
        Camerascript.camerascript.SwitchPOVmode(false);
    }
    public void Xfreecam()
    {
        xfreelookcamera.Priority = 1;
        freelookcamera.Priority = 0;
        povcamera.Priority = 0;
        Camerascript.camerascript.SwitchPOVmode(false);
    }
    public void Freecam()
    {
        xfreelookcamera.Priority = 0;
        freelookcamera.Priority = 1;
        povcamera.Priority = 0;
        Camerascript.camerascript.SwitchPOVmode(false);
    }
    public void Povcam()
    {
        xfreelookcamera.Priority = 0;
        freelookcamera.Priority = 0;
        povcamera.Priority = 1;
        Camerascript.camerascript.SwitchPOVmode(true);
    }
    public void Onxfreecam(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Xfreecam();
        }
    }
    public void Onfreecam(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Freecam();
        }
    }
    public void Onpovcam(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Povcam();
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
        if (!animator.GetBool(IsdyingHash))
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
        if (istheredropweapon)
        {
            isweaponnear = false;
            if (distances.Count == 0)
            {
                istheredropweapon = false;
            }
            if (istheredropweapon)
            {
                foreach (var dropweapon in distances)
                {
                    if (dropweapon.Value < weapongrabdistance)
                    {
                        isweaponnear = true;
                    }
                }
            }
            Ebutton.reference.gameObject.SetActive(isweaponnear);
        }
        fpscount++;
        if (Time.time - lastfpsshow > 1f)
        {
            fps.text = Convert.ToString(fpscount);
            fpscount = 0;
            lastfpsshow = Time.time;
        }
    }
}
