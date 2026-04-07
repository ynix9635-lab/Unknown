using UnityEngine;
using UnityEngine.UI;
public class Weaponpanel : MonoBehaviour
{
    [SerializeField] Button basicswordbutton;
    [SerializeField] Button macebutton;
    [SerializeField] Button fistbutton;
    [SerializeField] Button equipped;
    void Start()
    {
        basicswordbutton.onClick.AddListener(EquipBasicSword);
        fistbutton.onClick.AddListener(Unequip);
        macebutton.onClick.AddListener(Equipmace);
    }
    void EquipBasicSword()
    {
        MCC.mcc.Equipbasicsword();
        equipped.transform.position = basicswordbutton.transform.position;
    }
    void Equipmace()
    {
        MCC.mcc.Equipmace();
        equipped.transform.position = macebutton.transform.position;
    }
    void Unequip()
    {
        MCC.mcc.Unequipweapon();
        equipped.transform.position = fistbutton.transform.position;
    }
}
