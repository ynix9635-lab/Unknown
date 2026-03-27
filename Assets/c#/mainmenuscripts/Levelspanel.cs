using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levelspanel : MonoBehaviour
{
    [SerializeField] Button backbutton;
    [SerializeField] Button lvl1;
    void Awake()
    {
        lvl1.onClick.AddListener(Loadlvl1);
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
}
