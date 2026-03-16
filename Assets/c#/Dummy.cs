using UnityEngine;
[RequireComponent(typeof(Animator))]
public class Dummy : MonoBehaviour, IDamageable
{
    const float Maxhealth = 10;
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
}
