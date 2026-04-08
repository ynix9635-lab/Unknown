using UnityEngine;
using UnityEngine.UI;

public class Mainmenusettings : MonoBehaviour
{
    [SerializeField] Button backbutton;
    [SerializeField] Button povbutton;
    [SerializeField] Button freebutton;
    [SerializeField] Button Xfreebutton;
    void Start()
    {
        backbutton.onClick.AddListener(Back);
        povbutton.onClick.AddListener(Pov);
        freebutton.onClick.AddListener(Free);
        Xfreebutton.onClick.AddListener(Xfree);
    }
    void Back()
    {
        gameObject.SetActive(false);
        PlayerPrefs.Save();

    }
    void Pov()
    {
        PlayerPrefs.SetString("defaultcam","Pov");
    }
    void Free()
    {
        PlayerPrefs.SetString("defaultcam", "Free");
    }
    void Xfree()
    {
        PlayerPrefs.SetString("defaultcam", "Xfree");
    }
}
