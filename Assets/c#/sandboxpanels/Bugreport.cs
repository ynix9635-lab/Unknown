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
        webhookURL = "https://discord.com/api/webhooks/1485895722061529088/X9XFOsLLuahf9lg1M3nMqgAMjEgOOD3l1gJk87piSIRndmWaUUBfM1_dYznhLJoq4O6J";
        bugreport = this;
    }
    public void Takedamage(float damage)
    {
        Gamemanagement.reference.Switchactionmap("UI");
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
        string json = $"{{\"content\": \"new report \", \"embeds\": [{{\"title\": \"bug or feature suggestion\", \"description\": \"{text}\", \"color\": 16711680}}]}}";
        using UnityWebRequest www = new(webhookURL, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        webhookinfopanel.SetActive(true);
        Gamemanagement.reference.Switchactionmap("UI");
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        if (www.result != UnityWebRequest.Result.Success)
        {
            infotext.text = www.error;
        }
        else
        {
            infotext.text = "report is uploaded do devs discord";
        }
    }
}