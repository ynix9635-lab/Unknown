using UnityEngine;
[RequireComponent(typeof(Animator))]
public class Dummy : MonoBehaviour, IDamageable, IKickable
{
    Vector3 baseposition = new(0f, 0f, 5f);
    Vector3 inertia;
    const float Maxhealth = 100;
    float health = Maxhealth;
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Takedamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Respawn()
    {
        transform.position = baseposition;
        gameObject.SetActive(true);
        health = Maxhealth;
    }
    public void Die()
    {
        animator.SetTrigger("Death");
    }
    void Died()
    {
        gameObject.SetActive(false);
    }

    public void Getkicked(float kickpower)
    {
        inertia = MCC.reference.transform.forward * kickpower;
    }
    void Update()
    {
        inertia.x -= inertia.x * Time.deltaTime * 2;
        inertia.z -= inertia.z * Time.deltaTime * 2;
        if ((inertia.x < 0.001f && inertia.z < 0.001f && inertia.x > -0.001f && inertia.z > -0.001f) || Time.timeScale == 0f)
        {
            inertia.x = 0f;
            inertia.z = 0f;
        }
        transform.position = transform.position + inertia * Time.deltaTime;
    }
}
