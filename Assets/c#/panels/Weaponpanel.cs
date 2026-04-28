using UnityEngine;
using UnityEngine.UI;
public class Weaponpanel : MonoBehaviour
{
    [SerializeField] Button basicswordbutton;
    [SerializeField] Button macebutton;
    [SerializeField] Button fistbutton;
    [SerializeField] Button royalgreatswordbutton;
    [SerializeField] Button equipped;
    void Start()
    {
        royalgreatswordbutton.onClick.AddListener(EquipRoyalGreatsword);
        basicswordbutton.onClick.AddListener(EquipBasicSword);
        fistbutton.onClick.AddListener(Unequip);
        macebutton.onClick.AddListener(Equipmace);
    }
    void EquipRoyalGreatsword()
    {
        MCC.reference.Equiproyalgreatsword();
        equipped.transform.position = royalgreatswordbutton.transform.position;
    }
    void EquipBasicSword()
    {
        MCC.reference.Equipbasicsword();
        equipped.transform.position = basicswordbutton.transform.position;
    }
    void Equipmace()
    {
        MCC.reference.Equipmace();
        equipped.transform.position = macebutton.transform.position;
    }
    void Unequip()
    {
        MCC.reference.Unequipweapon();
        equipped.transform.position = fistbutton.transform.position;
    }
}
