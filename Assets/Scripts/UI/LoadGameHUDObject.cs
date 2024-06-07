using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameHUDObject : HUDObject
{

    public GameSaveAndLoad gameLoader;

    private void Start()
    {
        BaseStart();
    }

    protected override void OnMouseDown()
    {
        gameLoader.LoadGame("Tulpamancy");
        switch (GameData.Instance.gameDataStruct.screenID)
        {
            case ScreenID.ApartmentFrontDoor:
                SceneManager.LoadScene("FrontDoorApartment");
                break;
            case ScreenID.ApartmentHallway:
                SceneManager.LoadScene("Hallway");
                break;
            case ScreenID.Bedroom:
                SceneManager.LoadScene("Bedroom");
                break;
            case ScreenID.Kitchen:
                SceneManager.LoadScene("Kitchen");
                break;
        }
    }

}
