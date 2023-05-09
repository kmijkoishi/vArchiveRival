using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class UserInfoSLS : MonoBehaviour
{
    public static void SaveData(UserInfo userInfo)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/userInfo.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        UserInfo data = new UserInfo(userInfo);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static UserInfo LoadData()
    {
        string path = Application.persistentDataPath + "/userInfo.txt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            UserInfo data = formatter.Deserialize(stream) as UserInfo;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }
}
