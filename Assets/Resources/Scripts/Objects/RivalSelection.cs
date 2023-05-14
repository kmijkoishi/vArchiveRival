using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RivalSelection : MonoBehaviour
{
    // Start is called before the first frame update
    public Button selectButton;
    public Button deleteButton;
    public string rivalUrlName;
    private vArchiveCrawler archiveCrawler;
    public UserData rivalData;
    void Start()
    {
        archiveCrawler = vArchiveCrawler.instance;
        selectButton.onClick.AddListener(delegate { StartCoroutine(CompareToUser()); });
        deleteButton.onClick.AddListener(delegate { DeleteRival(); });
    }

    IEnumerator GetRivalData()
    {
        yield return StartCoroutine(archiveCrawler.GetUserData(rivalUrlName, UserDataManager.button, UserDataManager.board));
        rivalData = archiveCrawler.resultUserData;
    }

    IEnumerator CompareToUser()
    {
        yield return StartCoroutine(GetRivalData());
        yield return StartCoroutine(archiveCrawler.GetUserData(UserDataManager.userUrlName, UserDataManager.button, UserDataManager.board));
        UserData userData = archiveCrawler.resultUserData;
        UserDataManager.currentRivalUrlName = rivalUrlName;
        UserDataManager.Compare(userData, rivalData);
    }

    public void DeleteRival()
    {
        UserDataManager.rivalUrlNames.Remove(rivalUrlName);
        Destroy(this.gameObject);
    }
}
