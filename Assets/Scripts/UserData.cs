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