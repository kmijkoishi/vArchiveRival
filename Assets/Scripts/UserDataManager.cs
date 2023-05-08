using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UserDataManager : MonoBehaviour
{
    public static UserData myData;
    public UserData currentMyData;
    public List<UserData> rivalDatas;
    public UserData currentRivalData;

    public InputField myUrlInputField;
    public InputField rivalUrlInputField;

    public List<GameObject> rivalDataButtons;

    public GameObject rivalSelectionPrefab;

    public Transform scrollTransform;

    public vArchiveCrawler archiveCrawler;
    void Start()
    {
        archiveCrawler = vArchiveCrawler.instance;
        myUrlInputField.onSubmit.AddListener(delegate(string url) { StartCoroutine(GetMyUserData(url)); });
        rivalUrlInputField.onSubmit.AddListener(delegate(string url) { StartCoroutine(AddRivalUserData(url)); });
    }

    public IEnumerator GetMyUserData(string url)
    {
        yield return StartCoroutine(archiveCrawler.GetUserData(url));
        myData = archiveCrawler.tempUserData;
        currentMyData = myData;
    }

    public IEnumerator AddRivalUserData(string url)
    {
        yield return StartCoroutine(archiveCrawler.GetUserData(url));
        var rivalDataButtonObject = Instantiate(rivalSelectionPrefab, scrollTransform);
        RivalButton rivalButton = rivalDataButtonObject.GetComponent<RivalButton>();
        rivalButton.rivalData = archiveCrawler.tempUserData; 
        rivalDataButtons.Add(rivalDataButtonObject);
    }

    public static Dictionary<int, string> getScoreList(UserData data)
    {
        Dictionary<int, string> result = new Dictionary<int, string>();
        foreach (var floor in data.floors)
        {
            foreach (var pattern in floor.patterns)
            {
                result.Add(pattern.title, pattern.score);
            }
        }
        return result;
    }

    public static void Compare(UserData data, UserData target)
    {
        Dictionary<int, string> dataScoreList = getScoreList(data);
        Dictionary<int, string> targetScoreList = getScoreList(target);
        foreach (var key in dataScoreList.Keys)
        {
            if (String.IsNullOrEmpty(dataScoreList[key]) || String.IsNullOrEmpty(targetScoreList[key]))
            {
                continue;
            }
            float scoreDelta = ((int)(float.Parse(dataScoreList[key]) * 100 + 0.5f) - (int)(float.Parse(targetScoreList[key]) * 100 + 0.5f)) / 100f;
            Debug.Log(scoreDelta);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
