using System;
using UnityEngine;

public class BarrelScript : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject brokenbarrel;
    [SerializeField] Dropweapon dropweapon;
    public void Takedamage(float damage)
    {
        gameObject.SetActive(false);
        brokenbarrel.transform.position = transform.position + new Vector3(0,0.05f,0);
        brokenbarrel.transform.rotation = transform.rotation;
        brokenbarrel.SetActive(true); 
        dropweapon.transform.position = transform.position;
        dropweapon.gameObject.SetActive(true);
        foreach (Rigidbody rb in brokenbarrel.GetComponentsInChildren<Rigidbody>())
        {
            rb.AddExplosionForce(10f,transform.position,3f);
        }
    }
}
