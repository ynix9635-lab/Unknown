using Cinemachine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    Vector3 rollvector;
    Vector3 normalsize = new(1f, 1f, 1f);
    Vector3 movevector;
    Vector3 inertia;
    Vector3 baseposition = new(0f, 2.5f, 0f);
    Quaternion targetrotation;
    float spheresize = 0.4f;
    float maxstamina = 100f;
    float stamina;
    float maxhealth = 10f;
    float health;
    float lastjumptime;
    float lastkicktime;
    float laststaminadraintime;
    float speed;
    float g = 20;
    float speedmultiplier = 1f;
    float lastgroundedtime;
    const float stillgrounded = 0.2f;
    const float normalspheresize = 0.4f;
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
    bool airjump = true;
    bool isGrounded;
    bool iskickcd = true;
    bool isjumpcd = true;
    bool canattack = true;
    public bool Iscrouch { get; private set; }
    static readonly int WeaponHash = Animator.StringToHash("weapon");
    static readonly int SetAnimatorHash = Animator.StringToHash("setanimator");
    static readonly int DeathHash = Animator.StringToHash("death");
    static readonly int CrouchHash = Animator.StringToHash("crouch");
    static readonly int AttackHash = Animator.StringToHash("attack");
    static readonly int IsrunningHash = Animator.StringToHash("isrunning");
    static readonly int KickHash = Animator.StringToHash("kick");
    static readonly int JumpHash = Animator.StringToHash("jump");
    static readonly int IsdyingHash = Animator.StringToHash("isdying");
    Animator animator;
    CharacterController controller;
    [SerializeField] Collider triggercollider;
    [SerializeField] Transform groundCheck;
    [SerializeField] Image jumpcdfill;
    [SerializeField] Image kickcdfill;
    [SerializeField] GameObject jumpcdicon;
    [SerializeField] GameObject kickcdicon;
    [SerializeField] Image staminafill;
    [SerializeField] Image hpfill;
    [SerializeField] GameObject basicsword;
    [SerializeField] GameObject royalgreatsword;
    [SerializeField] GameObject mace;
    static public MCC reference;

    //methods
    void Awake()
    {
        stamina = maxstamina;
        health = maxhealth;
        reference = this;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        speed = walkspeed;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void OnEnable()
    {
        movevector.y = 0;
        movevector.x = 0;
        movevector.z = 0;
    }
    public void Onequip(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Gamemanagement.reference.OnEquip();
        }
    }
    public void Equipbasicsword()
    {
        Unequipweapon();
        basicsword.SetActive(true);
        animator.SetInteger(WeaponHash, 1);
        animator.SetTrigger(SetAnimatorHash);
    }
    public void Equipmace()
    {
        Unequipweapon();
        mace.SetActive(true);
        animator.SetInteger(WeaponHash, 2);
        animator.SetTrigger(SetAnimatorHash);
    }
    public void Equiproyalgreatsword()
    {
        Unequipweapon();
        royalgreatsword.SetActive(true);
        animator.SetInteger(WeaponHash, 3);
        animator.SetTrigger(SetAnimatorHash);
    }
    public void Unequipweapon()
    {
        animator.SetTrigger(SetAnimatorHash);
        animator.SetInteger(WeaponHash, 0);
        basicsword.SetActive(false);
        mace.SetActive(false);
        royalgreatsword.SetActive(false);
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && canattack && stamina > 0)
        {
            Changestamina(staminattackdrain);
            Iscrouch = false;
            animator.SetTrigger(AttackHash);
            canattack = false;
            animator.SetBool(CrouchHash, false);
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
            if (!Iscrouch)
            {
                speed = crouchspeed;
                Iscrouch = true;
                animator.SetBool(CrouchHash, true);
            }
            else
            {
                speed = walkspeed;
                Iscrouch = false;
                animator.SetBool(CrouchHash, false);
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
        if (!Iscrouch)
        {
            if (context.started || context.performed)
            {
                animator.SetBool(IsrunningHash, true);
                speed = runspeed;
            }
            else
            {
                animator.SetBool(IsrunningHash, false);
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
            Iscrouch = false;
            iskickcd = true;
            animator.SetBool(CrouchHash, false);
            animator.SetTrigger(KickHash);
        }
    }
    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("roll");
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
            if ((isGrounded || Time.time - lastgroundedtime < stillgrounded)&& Time.time - lastjumptime > jumpcd)
            {
                isjumpcd = true;
                jumpcdicon.SetActive(true);
                Changestamina(staminajumpdrain);
                animator.SetTrigger(JumpHash);
                airjump = true;
                movevector.y = jh * 20 * 2f;
                inertia = movevector*1.5f;
                inertia.y = 0f;
                lastjumptime = Time.time;
                Iscrouch = false;
                animator.SetBool(CrouchHash, false);
            }
            else if (stamina > 0 && airjump && Time.time - lastjumptime > jumpinterval && !isGrounded)
            {
                airjump = false;
                animator.SetTrigger(JumpHash);
                Changestamina(staminajumpdrain);
                inertia = inertia / 2f + ((moveinput.x * camright + camfwd * moveinput.y) * speed) / 1.5f;
                inertia.y = 0f;
                movevector.y = (jh * 20 * 2f) * ajm;
            }
        }
    }
    public void Setscale(float scale)
    {
        if(scale > 0.3f)
        {
            controller.Move(baseposition - transform.position);
        }
        transform.localScale = normalsize * scale;
        spheresize = normalspheresize * scale;
    }
    public void Setgravity(float value)
    {
        g = value;
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
    void CanAttack()
    {
        canattack = true;
    }
    public void Die()
    {
        animator.SetBool(IsdyingHash,true);
        animator.SetTrigger(DeathHash);
    }
    void Died()
    {
        animator.SetBool(IsdyingHash, false);
        Gamemanagement.reference.OnHeroDeath();
        gameObject.SetActive(false);
    }
    void Changestamina(float value)
    {
        stamina += value;
        staminafill.fillAmount = stamina / maxstamina;
        laststaminadraintime = Time.time;
    }
     public void Setspeedmultiplier(float speedmultiplier)
    {
        this.speedmultiplier = speedmultiplier;
    }
    void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("roll"))
        {
            rollvector.x = 0;
            rollvector.y = 0;
            rollvector.z = 0;
            triggercollider.enabled = true;
        }
        else
        {
            triggercollider.enabled = false;
            rollvector = transform.forward * 5f;
        }
        if (speed == runspeed && moveinput.magnitude > 0)
        {
            Changestamina(staminarundrain * Time.deltaTime);
            if(stamina < 0f)
            {
                speed = walkspeed;
                animator.SetBool(IsrunningHash, false);
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
        inputtovector = ((moveinput.x * camright + camfwd * moveinput.y) * speed) * speedmultiplier;
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("jump") && !animator.GetAnimatorTransitionInfo(0).IsName("RunJump -> idle") && !animator.GetAnimatorTransitionInfo(0).IsName("JumpOne -> idle"))
        {
            inputtovector.x = 0f;
            inputtovector.z = 0f;
        }
        movevector = inputtovector + (movevector.y * transform.up) + inertia + rollvector;
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            movevector.z = 0f;
            movevector.x = 0f;
        }
        movedirection = (moveinput.y * camfwd + camright * moveinput.x).normalized;
        isGrounded = Physics.CheckSphere(groundCheck.position, spheresize, ~LayerMask.GetMask("Hero"));
        if (isGrounded)
        {
            lastgroundedtime = Time.time;
        }
        if(movevector.y < -25f || transform.position.y < -10f)
        {
            Takedamage(maxhealth);
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