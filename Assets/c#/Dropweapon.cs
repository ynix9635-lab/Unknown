using UnityEngine;
using UnityEngine.Events;

public class Dropweapon : MonoBehaviour
{
    [SerializeField] UnityEvent method;
    private void Update()
    {
        Gamemanagement.reference.Changedistance(this,Vector3.Distance(transform.position,MCC.reference.gameObject.transform.position));
    }
    public void Take()
    {
        method.Invoke();
    }
}
