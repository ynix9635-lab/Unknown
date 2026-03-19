using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(Collider))]
public class Feetforkick : MonoBehaviour
{
    [SerializeField] float kickpower;
    Collider feet;
    bool ishitboxon = false;
    [SerializeField] GameObject holder;
    Animator holderanimator;
    List<GameObject> alreadyhit = new();
    private void Awake()
    {
        holderanimator = holder.GetComponent<Animator>();
        feet = GetComponent<Collider>();
    }
    void Enablefeetcollider()
    {
        alreadyhit.Clear();
        feet.enabled = true;
    }
    void Disablefeetcollider()
    {
        feet.enabled = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy") && !alreadyhit.Contains(other.gameObject))
        {
            IKickable hitobject = other.gameObject.GetComponent<IKickable>();
            hitobject.Getkicked(kickpower);
            alreadyhit.Add(other.gameObject);
        }
    }
    private void Update()
    {
        if (holderanimator.GetCurrentAnimatorStateInfo(0).IsTag("kick") && !ishitboxon)
        {
            Enablefeetcollider();
            ishitboxon = true;
        }
        else if (!holderanimator.GetCurrentAnimatorStateInfo(0).IsTag("kick"))
        {
            Disablefeetcollider();
            ishitboxon = false;
        }
    }

}