using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RivalButton : MonoBehaviour
{
    // Start is called before the first frame update
    public Button selectButton;
    public Button deleteButton;
    public UserData rivalData;
    void Start()
    {
        selectButton.onClick.AddListener(delegate { UserDataManager.Compare(UserDataManager.myData, rivalData); });
    }
}
