using UnityEngine;
using UnityEngine.UI;

public class Levelmanagement : MonoBehaviour
{
    [SerializeField] Image progressbarfill;
    [SerializeField] GameObject completelevelpanel;
    [SerializeField] Button restartbutton;
    [SerializeField] Button mainmenubutton;
    [SerializeField] Button nextlevelbutton;
    [SerializeField] float enemycount;
    float enemykilled;
    public static Levelmanagement levelmanagement;
    void Awake()
    {
        levelmanagement = this;
    }
    void Start()
    {
        restartbutton.onClick.AddListener(Gamemanagement.reference.ResetScene);
        mainmenubutton.onClick.AddListener (Gamemanagement.reference.Loadmainmenu);
        nextlevelbutton.onClick.AddListener(Gamemanagement.reference.Loadmainmenu);
    }
    public void Progress()
    {
        enemykilled += 1;
        progressbarfill.fillAmount = enemykilled/enemycount;
        if(enemykilled >= enemycount)
        {
            completelevelpanel.SetActive(true);
            Gamemanagement.reference.Switchactionmap("UI");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
    }
}
