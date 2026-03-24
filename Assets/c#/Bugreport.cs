using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;


public class Bugreport : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject takebugreport;
    [SerializeField] GameObject webhookinfopanel;
    [SerializeField] TMP_Text infotext;
    static public Bugreport bugreport;
    string webhookURL;
    void Awake()
    {
        webhookURL = "https://discord.com/api/webhooks/1485618948677566476/_kteQ847J8CwmskaQ3_9XAlDnLU-ei2NBfY1FagaP-zCFQKrYr8YM3AjjrrBHF5xftyh";//yes u can spam my discord server easily having this link BUT why would you? this is a fully free game its fully open source so why? :)
        bugreport = this;
    }
    public void Takedamage(float damage)
    {
        Gamemanagement.gamemanagement.switchactionmap("UI");
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        Time.timeScale = 0f;
        takebugreport.SetActive(true);
    }

    public void SendBugReport(string message)
    {
        StartCoroutine(PostToDiscord(message));
    }

    IEnumerator PostToDiscord(string text)
    {
        string json = $"{{\"content\": \"Новый репорт \", \"embeds\": [{{\"title\": \"Отчет о баге\", \"description\": \"{text}\", \"color\": 16711680}}]}}";
        using UnityWebRequest www = new(webhookURL, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        webhookinfopanel.SetActive(true);
        Gamemanagement.gamemanagement.switchactionmap("UI");
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        if (www.result != UnityWebRequest.Result.Success)
        {
            infotext.text = www.error;
        }
        else
        {
            infotext.text = "Баг-репорт отправлен в Discord!";
        }
    }
}