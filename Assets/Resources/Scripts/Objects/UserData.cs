using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{ 
    public string success;
    public string board;
    public string button;
    public int totalCount;
    public Floor[] floors;
}

[Serializable]
public class Floor
{
    public float floorNumber;
    public Pattern[] patterns;

}

[Serializable]
public class Pattern
{
    public int title;
    public string name;
    public string composer;
    public string pattern;
    public string score;
    public short maxCombo;
    public string dlc;
    public string dlcCode;
}

public class ResultDataList
{
    public List<ResultData> results = new List<ResultData>();
}
public class ResultData
{
    public int title;
    public string scoreDelta;
    public string name;
    public float floor;

    public ResultData(int title, string scoreDelta, string name, float floor)
    {
        this.title = title;
        this.scoreDelta = scoreDelta;
        this.name = name;
        this.floor = floor;
    }

    public ResultData(UserScore userScore, string scoreDelta)
    {
        this.title = userScore.title;
        this.scoreDelta = scoreDelta;
        this.name = userScore.name;
        this.floor = userScore.floor;
    }
}