using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserScoreList
{
    public List<UserScore> UserScores = new List<UserScore>();
}

public class UserScore
{
    public int title;
    public string score;
    public bool maxCombo;
    public string name;
    public float floor;

    public UserScore(Pattern pattern, float floor)
    {
        title = pattern.title;
        score = pattern.score;
        maxCombo = pattern.maxCombo >= 0;
        name = pattern.name;
        this.floor = floor;
    }

    public override string ToString()
    {
        return "title: " + title + " score: " + score + " maxCombo: " + maxCombo + " name: " + name;
    }
}