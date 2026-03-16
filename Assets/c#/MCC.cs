using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.VisualScripting;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Transform))]
public class MCC : MonoBehaviour
{
    //class fields
    Vector3 camfwd;
    Vector3 inputtovector;
    Vector3 camright;
    Vector3 movedirection;
    Vector2 moveinput;
    Vector3 checkboxhalf;
    Quaternion targetrotation;
    Vector3 movevector;
    bool airjump = true;
    bool isGrounded;
    float lastjumptime;
    float speed;
    const float jumpinterval = 0.2f;
    public const float jumpcd = 2f;
    Vector3 inertia;
    public bool iscrouch { get; private set; }
    const float runspeed = 5f;
    const float walkspeed = 3f;
    const float crouchspeed = 2f;
    const float jh = 0.15f;
    const float ajm = 1.1f;
    float g = 20;
    const float rotatespeed = 10f;
    Animator animator;
    CharacterController controller;
    [SerializeField]Transform groundCheck;
    static public MCC mcc;

    //methods
    void Awake()
    {
        checkboxhalf.x = 0.1f;
        checkboxhalf.y = 0.004f;
        checkboxhalf.z = 0.1f;
        mcc = this;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        speed = walkspeed;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && ((!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")
            && !animator.GetAnimatorTransitionInfo(0).IsName("idle -> Attack")
            && !animator.GetAnimatorTransitionInfo(0).IsName("walk -> Attack")
            && !animator.GetAnimatorTransitionInfo(0).IsName("Crouchidle -> Attack")
            && !animator.GetAnimatorTransitionInfo(0).IsName("CrouchWalk -> Attack"))
            || animator.GetAnimatorTransitionInfo(0).IsName("Attack -> idle")))
        {
            iscrouch = false;
            animator.SetTrigger("attack");
            animator.SetBool("crouch", false);
        }
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
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            if (context.performed)
            {
                if (isGrounded && Time.time - lastjumptime > jumpcd)
                {
                    animator.SetTrigger("jump");
                    airjump = true;
                    Gamemanagement.gamemanagement.OnJump();
                    movevector.y = jh * g * 2f;
                    inertia = movevector*1.5f;
                    inertia.y = 0f;
                    lastjumptime = Time.time;
                    iscrouch = false;
                    animator.SetBool("crouch", false);
                }
                else if (airjump && Time.time - lastjumptime > jumpinterval && !isGrounded)
                {
                    airjump = false;
                    animator.SetTrigger("jump");
                    inertia = inertia / 2f + ((moveinput.x * camright + camfwd * moveinput.y) * speed) / 1.5f;
                    inertia.y = 0f;
                    movevector.y = (jh * g * 2f) * ajm;
                }
            }
        }
    }
    void Update()
    { 
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