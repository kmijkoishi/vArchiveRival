using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class UserDataManager : MonoBehaviour
{
    public static string userUrlName;
    public static List<string> rivalUrlNames = new List<string>();
    
    public static string currentRivalUrlName;

    public InputField userUrlInputField;
    public InputField rivalUrlInputField;

    public Button reloadButton;
    public Button saveButton;
    
    public GameObject rivalSelectionPrefab;

    public Transform scrollTransform;

    public vArchiveCrawler archiveCrawler;

    public static string button = "4";
    public static string board = "SC";
    
    
    void Start()
    {
        archiveCrawler = vArchiveCrawler.instance;
        userUrlInputField.onSubmit.AddListener(delegate(string url) { GetMyUserData(url); });
        rivalUrlInputField.onSubmit.AddListener(delegate(string url) { AddRivalUserData(url); });
        reloadButton.onClick.AddListener(delegate { StartCoroutine(ReLoadComparsion()); });
        saveButton.onClick.AddListener(delegate { SaveUserInfo(); });
        LoadUserInfo();
        UserInfoSLS.LoadGallary();
    }

    public IEnumerator ReLoadComparsion()
    {
        yield return StartCoroutine(archiveCrawler.GetUserData(userUrlName, button, board));
        UserData userData = archiveCrawler.resultUserData;
        yield return StartCoroutine(archiveCrawler.GetUserData(currentRivalUrlName, button, board));
        UserData rivalData = archiveCrawler.resultUserData;
        Compare(userData, rivalData);
    }

    public void GetMyUserData(string url)
    {
        userUrlName = url.Split('/')[4];
    }

    public void AddRivalUserData(string url)
    {
        string rivalName = url.Split('/')[4];
        if (!rivalUrlNames.Contains(rivalName))
        {
            rivalUrlNames.Add(rivalName);
            var rivalSelectionObject = Instantiate(rivalSelectionPrefab, scrollTransform);
            RivalSelection rivalSelection = rivalSelectionObject.GetComponent<RivalSelection>();
            rivalSelection.rivalUrlName = rivalName;
            rivalSelection.selectButton.transform.GetChild(0).GetComponent<Text>().text = Uri.UnescapeDataString(rivalName);
        }
    }

    public void ReLoadRivalSelection()
    {
        foreach (Transform child in scrollTransform)
        {
            Destroy(child);
        }

        foreach (var rival in rivalUrlNames)
        {
            var rivalSelectionObject = Instantiate(rivalSelectionPrefab, scrollTransform);
            RivalSelection rivalSelection = rivalSelectionObject.GetComponent<RivalSelection>();
            rivalSelection.rivalUrlName = rival;
            rivalSelection.selectButton.transform.GetChild(0).GetComponent<Text>().text = Uri.UnescapeDataString(rival);
        }
    }

    public void SaveUserInfo()
    {
        UserInfo userInfo = new UserInfo(userUrlName, rivalUrlNames);
        UserInfoSLS.SaveData(userInfo);
    }

    public void LoadUserInfo()
    {
        UserInfo userInfo = UserInfoSLS.LoadData();
        if (userInfo == null) return;
        userUrlName = userInfo.userUrlName;
        rivalUrlNames = userInfo.rivalUrlNames;
        ReLoadRivalSelection();
    }

    public static UserScoreList getScoreList(UserData data)
    {
        UserScoreList result = new UserScoreList();
        foreach (var floor in data.floors)
        {
            foreach (var pattern in floor.patterns)
            {
                UserScore score = new UserScore(pattern, floor.floorNumber);
                result.UserScores.Add(score);
            }
        }
        return result;
    }

    public static void Compare(UserData original, UserData target)
    {
        UserScoreList originalScoreList = getScoreList(original);
        UserScoreList targetScoreList = getScoreList(target);
        ResultDataList resultDataList = new ResultDataList();
        for (int i = 0; i < originalScoreList.UserScores.Count; i++)
        {
            var originalUserScore = originalScoreList.UserScores[i];
            var targetUserScore = targetScoreList.UserScores[i];
            if (String.IsNullOrEmpty(originalUserScore.score) || String.IsNullOrEmpty(targetUserScore.score))
            {
                ResultData data = new ResultData(originalUserScore, null);
                resultDataList.results.Add(data);
            }
            else
            {
                float scoreDelta = ((int)(float.Parse(originalUserScore.score) * 100 + 0.5f) - (int)(float.Parse(targetUserScore.score) * 100 + 0.5f)) / 100f;
                ResultData data = new ResultData(originalUserScore, scoreDelta.ToString());
                resultDataList.results.Add(data);
                /*
                Debug.Log("name: " + originalScoreList.UserScores[i].name);
                Debug.Log("title: " + originalScoreList.UserScores[i].title + ", " + targetScoreList.UserScores[i].title);
                Debug.Log("myScore: " + originalScoreList.UserScores[i].score + "/ targetScore: " + targetScoreList.UserScores[i].score);
                Debug.Log("scoreDelta: " + scoreDelta);
                Debug.Log("myUserScore: " + originalScoreList.UserScores[i]);
                Debug.Log("targetUserScore: " + targetScoreList.UserScores[i]);
                */
            }
        }
        MenuManager.LoadSongListVisual(resultDataList);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
