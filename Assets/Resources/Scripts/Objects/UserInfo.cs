using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[Serializable]
public class UserInfo
{
    public string userUrlName;
    public List<string> rivalUrlNames;

    public UserInfo(string userUrlName, List<string> rivalUrlNames)
    {
        this.userUrlName = userUrlName;
        this.rivalUrlNames = rivalUrlNames;
    }
    public UserInfo(UserInfo userInfo)
    {
        this.userUrlName = userInfo.userUrlName;
        this.rivalUrlNames = userInfo.rivalUrlNames;
    }
}
