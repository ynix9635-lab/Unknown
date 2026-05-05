using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levelspanel : MonoBehaviour
{
    [SerializeField] Button backbutton;
    [SerializeField] Button lvl1;
    [SerializeField] Button lvl2;
    [SerializeField] Button lvl3;
    [SerializeField] Button lvl4;
    void Awake()
    {
        lvl1.onClick.AddListener(Loadlvl1);
        lvl2.onClick.AddListener(Loadlvl2);
        lvl3.onClick.AddListener(Loadlvl3);
        lvl4.onClick.AddListener(Loadlvl4);
        backbutton.onClick.AddListener(Back);
    }
    void Back()
    {
        gameObject.SetActive(false);
    }
    void Loadlvl1()
    {
        SceneManager.LoadScene("lvl1");
    }
    void Loadlvl2()
    {
        SceneManager.LoadScene("lvl2");
    }
    void Loadlvl3()
    {
        SceneManager.LoadScene("lvl3");
    }
    void Loadlvl4()
    {
        SceneManager.LoadScene("lvl4");
    }
}
