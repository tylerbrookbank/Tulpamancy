using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public struct GameDataStruct
{
    public string Name;
    public ScreenID screenID;
    public BedroomEventStruct bedroomEventStruct;
    public KitchenGameState kitchenGameState;
    public List<itemStructForSaving> items;
    public short drankBeerCount;
    public bool drankShot;
}

public class GameData : MonoBehaviour
{

    public static GameData Instance;
    public GameDataStruct gameDataStruct;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        gameDataStruct = new GameDataStruct();
        gameDataStruct.Name = "Tulpamancy";
        if (gameDataStruct.items == null)
            gameDataStruct.items = new List<itemStructForSaving>();
    }

}
