using UnityEngine;
using UnityEngine.UI;
public class Weaponpanel : MonoBehaviour
{
    [SerializeField] Button basicswordbutton;
    [SerializeField] Button fistbutton;
    void Start()
    {
        basicswordbutton.onClick.AddListener(MCC.mcc.Equipbasicsword);
        fistbutton.onClick.AddListener(MCC.mcc.Unequipweapon);
    }
}
