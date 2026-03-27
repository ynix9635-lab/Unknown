using UnityEngine;

public class Spinningobject : MonoBehaviour
{
    Quaternion constantrotation;
    private void Start()
    {
        constantrotation = Quaternion.Euler(0.5f,4,1);
    }
    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,constantrotation*transform.rotation,Time.deltaTime*10);
    }
}
