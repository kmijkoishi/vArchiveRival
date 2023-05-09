using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public Dropdown dropDown;
    public void ChangeButton()
    {
        Toggle toggle = toggleGroup.ActiveToggles().FirstOrDefault();
        switch (toggle.name)
        {
            case "Button4Key":
                UserDataManager.button = "4";
                break;
            case "Button5Key":
                UserDataManager.button = "5";
                break;
            case "Button6Key":
                UserDataManager.button = "6";
                break;
            case "Button8Key":
                UserDataManager.button = "8";
                break;
            default:
                UserDataManager.button = "4";
                break;
        }
        Debug.Log("currentButton: " + UserDataManager.button);
    }

    public void ChangeBoard(int boardIndex)
    {
        switch (boardIndex)
        {
            case 0: UserDataManager.board = "1"; break;
            case 1: UserDataManager.board = "2"; break;
            case 2: UserDataManager.board = "3"; break;
            case 3: UserDataManager.board = "4"; break;
            case 4: UserDataManager.board = "5"; break;
            case 5: UserDataManager.board = "6"; break;
            case 6: UserDataManager.board = "7"; break;
            case 7: UserDataManager.board = "8"; break;
            case 8: UserDataManager.board = "9"; break;
            case 9: UserDataManager.board = "10"; break;
            case 10: UserDataManager.board = "11"; break;
            case 11: UserDataManager.board = "MX"; break;
            case 12: UserDataManager.board = "SC"; break;
            case 13: UserDataManager.board = "SC5"; break;
            case 14: UserDataManager.board = "SC10"; break;
            case 15: UserDataManager.board = "SC15"; break;
        }
        Debug.Log("currentBoard: " + UserDataManager.board);
    }
    // Start is called before the first frame update
    void Start()
    {
        dropDown.onValueChanged.AddListener(delegate(int board) { ChangeBoard(board); });
        dropDown.SetValueWithoutNotify(12);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
