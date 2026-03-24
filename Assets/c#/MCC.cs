using Cinemachine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class MCC : MonoBehaviour, IDamageable
{
    //class fields
    Vector3 camfwd;
    Vector3 inputtovector;
    Vector3 camright;
    Vector3 movedirection;
    Vector2 moveinput;
    Vector3 checkboxhalf;
    Vector3 normalsize = new(1f, 1f, 1f);
    Vector3 checkboxhalfnormal = new(0.1f, 0.004f, 0.1f);
    Vector3 movevector;
    Vector3 inertia;
    Vector3 baseposition = new(0f, 2.5f, 0f);
    Quaternion targetrotation;
    float maxstamina = 100f;
    float stamina;
    float maxhealth = 10f;
    float health;
    float lastjumptime;
    float lastkicktime;
    float laststaminadraintime;
    float speed;
    float g = 20;
    const float staminarecoveryspeed = 5f;
    const float staminarundrain = -5f;
    const float staminakickdrain = -10f;
    const float staminattackdrain = -25f;
    const float staminajumpdrain = -5f;
    const float jumpinterval = 0.2f;
    const float kickcd = 4f;
    const float jumpcd = 2f;
    const float runspeed = 5f;
    const float walkspeed = 3f;
    const float crouchspeed = 2f;
    const float jh = 0.15f;
    const float ajm = 1.1f;
    const float rotatespeed = 10f;
    Animator animator;
    CharacterController controller;
    bool airjump = true;
    bool isGrounded;
    bool iskickcd = true;
    bool isjumpcd = true;
    public bool iscrouch { get; private set; }
    [SerializeField] Transform groundCheck;
    [SerializeField] Image jumpcdfill;
    [SerializeField] Image kickcdfill;
    [SerializeField] GameObject jumpcdicon;
    [SerializeField] GameObject kickcdicon;
    [SerializeField] Image staminafill;
    [SerializeField] Image hpfill;
    static public MCC mcc;

    //methods
    void Awake()
    {
        stamina = maxstamina;
        health = maxhealth;
        checkboxhalf = checkboxhalfnormal;
        mcc = this;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        speed = walkspeed;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && ((!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")
            && !animator.GetAnimatorTransitionInfo(0).IsName("idle -> Attack")
            && !animator.GetAnimatorTransitionInfo(0).IsName("walk -> Attack")
            && !animator.GetAnimatorTransitionInfo(0).IsName("Crouchidle -> Attack")
            && !animator.GetAnimatorTransitionInfo(0).IsName("CrouchWalk -> Attack"))
            || animator.GetAnimatorTransitionInfo(0).IsName("Attack -> idle"))
            && stamina > 0)
        {
            Changestamina(staminattackdrain);
            iscrouch = false;
            animator.SetTrigger("attack");
            animator.SetBool("crouch", false);
        }
    }
    public void Setmaxhp(float value)
    {
        maxhealth = value;
        health = maxhealth;
        hpfill.fillAmount = 1f;
    }
    public void Setmaxstamina(float value)
    {
        maxstamina = value;
        stamina = maxstamina;
        hpfill.fillAmount = 1f;
    }
    public void Respawn()
    {
        transform.position = baseposition;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        health = maxhealth;
        stamina = maxstamina;
        staminafill.fillAmount = 1f;
        hpfill.fillAmount = 1f;
        gameObject.SetActive(true);
        Time.timeScale = 1f;
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!iscrouch)
            {
                speed = crouchspeed;
                iscrouch = true;
                animator.SetBool("crouch", true);
            }
            else
            {
                speed = walkspeed;
                iscrouch = false;
                animator.SetBool("crouch", false);
            }
        }
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (g == 0f)
        {
            if (context.started || context.performed)
            {
                movevector.y = -2f;
            }
            else
            {
                movevector.y = 0f;
            }
            return;
        }
        if (!iscrouch)
        {
            if (context.started || context.performed)
            {
                animator.SetBool("isrunning", true);
                speed = runspeed;
            }
            else
            {
                animator.SetBool("isrunning", false);
                speed = walkspeed;
            }
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveinput = context.ReadValue<Vector2>().normalized;
    }
    public void ToggleFlyMode()
    {
        if(g == 0f)
        {
            g = 20f;
            speed = walkspeed;
        }
        else
        {
            g = 0f;
            speed = runspeed;
        }
    }
    public void OnKick(InputAction.CallbackContext context)
    {
        if (context.performed && Time.time - lastkicktime > kickcd && stamina > 0)
        {
            Changestamina(staminakickdrain);
            kickcdicon.SetActive(true);
            lastkicktime = Time.time;
            iscrouch = false;
            iskickcd = true;
            animator.SetBool("crouch", false);
            animator.SetTrigger("kick");
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(g == 0f)
        {
            if (context.started || context.performed)
            {
                movevector.y = 2f;
            }
            else
            {
                movevector.y = 0f;
            }
            return;
        }
        if (context.performed && stamina > 0 && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            if (isGrounded && Time.time - lastjumptime > jumpcd)
            {
                isjumpcd = true;
                jumpcdicon.SetActive(true);
                Changestamina(staminajumpdrain);
                animator.SetTrigger("jump");
                airjump = true;
                movevector.y = jh * g * 2f;
                inertia = movevector*1.5f;
                inertia.y = 0f;
                lastjumptime = Time.time;
                iscrouch = false;
                animator.SetBool("crouch", false);
            }
            else if (stamina > 0 && airjump && Time.time - lastjumptime > jumpinterval && !isGrounded)
            {
                airjump = false;
                animator.SetTrigger("jump");
                Changestamina(staminajumpdrain);
                inertia = inertia / 2f + ((moveinput.x * camright + camfwd * moveinput.y) * speed) / 1.5f;
                inertia.y = 0f;
                movevector.y = (jh * g * 2f) * ajm;
            }
        }
    }
    public void Setscale(float scale)
    {
        if(scale > 0.3f)
        {
            gameObject.SetActive(false);
            transform.position = transform.position + transform.up;
            gameObject.SetActive(true);
        }
        checkboxhalf = checkboxhalfnormal * scale;
        transform.localScale = normalsize * scale;
    }
    public void Takedamage(float damage)
    {
        health -= damage;
        hpfill.fillAmount = health / maxhealth;
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        animator.SetTrigger("Death");
    }
    void Died()
    {
        Gamemanagement.gamemanagement.OnHeroDeath();
        gameObject.SetActive(false);
    }
    void Changestamina(float value)
    {
        stamina += value;
        staminafill.fillAmount = stamina / maxstamina;
        laststaminadraintime = Time.time;
    }
    void Update()
    {
        if(speed == runspeed && moveinput.magnitude > 0)
        {
            Changestamina(staminarundrain * Time.deltaTime);
            if(stamina < 0f)
            {
                speed = walkspeed;
                animator.SetBool("isrunning",false);
            }
        }
        else if(stamina < maxstamina)
        {
            if(moveinput.magnitude > 0)
            {
                Changestamina(staminarecoveryspeed * Time.deltaTime);
            }
            else
            {
                Changestamina(staminarecoveryspeed * Time.deltaTime * 2.5f);
            }
        }
        if (isjumpcd)
        {
            if(Time.time - lastjumptime < jumpcd)
            {
                jumpcdfill.fillAmount = 1 - ((Time.time - lastjumptime) / jumpcd);
            }
            else
            {
                isjumpcd = false;
                jumpcdicon.SetActive(false);
            }
        }
        if (iskickcd)
        {
            if (Time.time - lastkicktime < kickcd)
            {
                kickcdfill.fillAmount = 1 - ((Time.time - lastkicktime) / kickcd);
            }
            else
            {
                iskickcd = false;
                kickcdicon.SetActive(false);
            }
        }
        camfwd = Camera.main.transform.forward;
        camright = Camera.main.transform.right;
        camfwd.y = 0;
        camfwd.Normalize();
        camright.y = 0;
        camright.Normalize();
        inputtovector = ((moveinput.x * camright + camfwd * moveinput.y) * speed);
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("jump") && !animator.GetAnimatorTransitionInfo(0).IsName("RunJump -> idle") && !animator.GetAnimatorTransitionInfo(0).IsName("JumpOne -> idle"))
        {
            inputtovector.x = 0f;
            inputtovector.z = 0f;
        }
        movevector = inputtovector + (movevector.y * transform.up) + inertia;
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            movevector.z = 0f;
            movevector.x = 0f;
        }
        movedirection = (moveinput.y * camfwd + camright * moveinput.x).normalized;
        isGrounded = Physics.CheckBox(groundCheck.position, checkboxhalf, transform.rotation, ~LayerMask.GetMask("Hero"));
        if(isGrounded && movevector.y < -10f)
        {
            Takedamage(-(movevector.y*0.2f));
        }
        animator.SetFloat("speed", moveinput.magnitude);
        if (isGrounded && movevector.y <= 0f)
        {
            movevector.y = 0f;
            airjump = true;
            inertia.x = 0f;
            inertia.z = 0f;
        }
        else
        {
            movevector.y += -g * Time.deltaTime;
        }
        controller.Move(movevector * Time.deltaTime);
        if (movedirection.sqrMagnitude > 0f)
        {
            targetrotation = Quaternion.LookRotation(movedirection);
            if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, rotatespeed*Time.deltaTime);
            }
        }
    }
}