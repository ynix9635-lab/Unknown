using UnityEngine;
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] float maxhealth;
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
    Animator animator;
    void Start()
    {
        health = maxhealth;
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
    private void Awake()
    {
        animator = GetComponent<Animator>();
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
    }
    public void Update()
    {
        AI();
    }
}
