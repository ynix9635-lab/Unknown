using UnityEngine;
using UnityEngine.UI;
public class Equipmentpanel : MonoBehaviour
{
    [SerializeField] Button weaponbutton;
    [SerializeField] GameObject weaponpanel;
    void Start()
    {
        weaponbutton.onClick.AddListener(Weapon);
    }
    void Closeallpanels()
    {
        weaponpanel.SetActive(false);
    }
    void Weapon()
    {
        Closeallpanels();
        weaponpanel.SetActive(true);
    }
}
