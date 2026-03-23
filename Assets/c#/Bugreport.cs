using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class Bugreport : MonoBehaviour, IDamageable
{
    public void Takedamage(float damage)
    {
        throw new System.NotImplementedException();
    }
    /*const string webhookURL = "";

    public void SendBugReport(string message)
    {
        StartCoroutine(PostToDiscord( message));
    }

    IEnumerator PostToDiscord( string text)
    {
        string json = "{ \"content\": \"Новый репорт " + "\", \"embeds\": [{ \"title\": \"Отчет о баге\", \"description\": \"" + text + "\", \"color\": 16711680 }] }";

        using (UnityWebRequest www = UnityWebRequest.Post(webhookURL, json))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.Log("Ошибка отправки: " + www.error);
            else
                Debug.Log("Отправлено в Discord");
        }
    }*/
}