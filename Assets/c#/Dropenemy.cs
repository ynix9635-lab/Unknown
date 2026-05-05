using UnityEngine;

public class Dropenemy : MonoBehaviour
{
    [SerializeField] Dropweapon dropweapon;
    private void OnDisable()
    {
        if (dropweapon != null)
        {
            dropweapon.transform.position = transform.position + transform.up*2;
            dropweapon.transform.rotation = transform.rotation;
            dropweapon.gameObject.SetActive(true);
        }
    }
}
