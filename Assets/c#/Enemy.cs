using JetBrains.Annotations;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour, IDamageable, IKickable
{
    [SerializeField] Transform groundcheck;
    [SerializeField] float maxhealth;
    Vector3 checkboxhalfnormal;
    Vector3 inertia;
    bool isgrounded;
    float health;
    float detectrange;
    const float detectcrouchrange = 5f;
    const float detectnotcrouchrange = 10f;
    Vector3 targetdirection;
    bool iseeplayer;
    Quaternion targetrotation;
    const float rotatespeed = 10f;
    const float detectangle = 120f;
    const float attackrange = 1f;
    const float chasespeed = 4f;
    const float g = 20;
    public Vector3 movevector;
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
        checkboxhalfnormal = new(0.1f, 0.004f, 0.1f);
    }
    void Start()
    {
        health = maxhealth;
    }
    public void Getkicked(float kickpower)
    {
        inertia = MCC.mcc.transform.forward * kickpower;
    }
    public void Takedamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        animator.SetTrigger("Death");
    }
    void Died()
    {
        gameObject.SetActive(false);
    }
    void Attack()
    {
        animator.SetTrigger("attack");
    }
    void AI()
    {
        if (MCC.mcc.iscrouch)
        {
            detectrange = detectcrouchrange;
        }
        else
        {
            detectrange = detectnotcrouchrange;
        }
        if (Vector3.Distance(transform.position, MCC.mcc.transform.position) < detectrange)
        {
            targetdirection = MCC.mcc.transform.position - transform.position;
            targetdirection.y = 0;
            if (detectrange == detectcrouchrange)
            {
                if (!(Vector3.Angle(transform.forward, targetdirection) > 0.5 * detectangle))
                {
                    iseeplayer = true;
                    targetrotation = Quaternion.LookRotation(targetdirection);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, rotatespeed * Time.deltaTime);
                }
                else
                {
                    iseeplayer = false;
                }
            }
            else
            {
                iseeplayer = true;
                targetrotation = Quaternion.LookRotation(targetdirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, rotatespeed * Time.deltaTime);
            }
        }
        else
        {
            iseeplayer = false;
        }
        if (iseeplayer && Vector3.Distance(transform.position, MCC.mcc.transform.position) > attackrange)
        {
            animator.SetBool("ischasing", true);
            transform.position += chasespeed * Time.deltaTime * transform.forward;
        }
        else
        {
            animator.SetBool("ischasing", false);
        }
        if(Vector3.Distance(transform.position, MCC.mcc.transform.position) <= attackrange && iseeplayer)
        {
            Attack();
        }
    }
    void Update()
    {
        isgrounded = Physics.CheckBox(groundcheck.position, checkboxhalfnormal, transform.rotation, ~LayerMask.GetMask("enemy"));
        if(isgrounded && movevector.y <= 0)
        {
            inertia.x -= inertia.x * Time.deltaTime * 2;
            inertia.z -= inertia.z * Time.deltaTime * 2;
            if (inertia.x < 0.01f && inertia.z < 0.01f)
            {
                inertia.x = 0f;
                inertia.z = 0f;
            }
            movevector.y = 0;
        }
        else
        {
            movevector.y += -g * Time.deltaTime;
        }
        transform.position += movevector * Time.deltaTime + inertia;
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            AI();
        }
    }
}
