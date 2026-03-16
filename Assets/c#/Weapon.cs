using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class Weapon : MonoBehaviour
{
    [SerializeField] float damage;
    Collider weapon;
    bool ishitboxon = false;
    [SerializeField] GameObject holder;
    Animator holderanimator;
    List<GameObject> alreadyhit = new();
    private void Awake()
    {
        holderanimator = holder.GetComponent<Animator>();
        weapon = GetComponent<Collider>();
    }
    void Enablefistcollider()
    {
        alreadyhit.Clear();
        weapon.enabled = true;
    }
    void Disablefistcollider()
    {
        weapon.enabled = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy") && !alreadyhit.Contains(other.gameObject))
        {
            IDamageable hitobject = other.gameObject.GetComponent<IDamageable>();
            hitobject.Takedamage(damage);
            alreadyhit.Add(other.gameObject);
        }
    }
    private void Update()
    {
        if (holderanimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !ishitboxon)
        {
            Enablefistcollider();
            ishitboxon=true;
        }
        else if (!holderanimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Disablefistcollider();
            ishitboxon = false;
        }
    }
}
