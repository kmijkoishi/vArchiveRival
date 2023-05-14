using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;


public class vArchiveCrawler : MonoBehaviour
{
    public static vArchiveCrawler instance;

    public void Awake()
    {
        instance = this;
    }

    public Action<string> OnError;
    public Action<string> OnSuccess;
    public UserData resultUserData;
    public IEnumerator GetUserData(string url, string button, string board)
    {
        string finalUrl = "https://v-archive.net/api/archive/" + url + "/board/" + button + "/" + board;
        using (UnityWebRequest result = UnityWebRequest.Get(finalUrl))
        {
            yield return result.SendWebRequest();

            if (result.result == UnityWebRequest.Result.ConnectionError || result.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(result.error);
            }
            else
            {
                string json = result.downloadHandler.text;
                resultUserData = JsonUtility.FromJson<UserData>(json);
                //OnSuccess(rawUTF8Text);
            }
            
        }
    }
}
