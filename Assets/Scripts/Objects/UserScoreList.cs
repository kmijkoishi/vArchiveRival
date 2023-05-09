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

    public UserScore(Pattern pattern)
    {
        title = pattern.title;
        score = pattern.score;
        maxCombo = pattern.maxCombo >= 0;
        name = pattern.name;
    }

    public override string ToString()
    {
        return "title: " + title + " score: " + score + " maxCombo: " + maxCombo + " name: " + name;
    }
}