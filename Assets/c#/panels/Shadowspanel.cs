using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Shadowspanel : MonoBehaviour
{
    [SerializeField] Button turnoffshadowsbutton;
    [SerializeField] Button softshadowsbutton;
    [SerializeField] Button hardshadowsbutton;
    [SerializeField] Button highshadowsbutton;
    [SerializeField] Button mediumshadowsbutton;
    [SerializeField] Button lowshadowsbutton;
    void Start()
    {
        turnoffshadowsbutton.onClick.AddListener(Sun.reference.Turnoffshadows);
        softshadowsbutton.onClick.AddListener(Sun.reference.Softshadows);
        hardshadowsbutton.onClick.AddListener(Sun.reference.Hardshadows);
        highshadowsbutton.onClick.AddListener(Sun.reference.Highshadows);
        mediumshadowsbutton.onClick.AddListener(Sun.reference.Mediumshadows);
        lowshadowsbutton.onClick.AddListener(Sun.reference.Lowshadows);
    }
}
