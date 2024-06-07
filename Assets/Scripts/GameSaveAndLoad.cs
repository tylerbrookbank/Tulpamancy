using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Video;

public struct itemStructForSaving
{
    public string name;
    public string description;
    public string itemSprite;
    public string video;
    public int id;
};

public class GameSaveAndLoad : MonoBehaviour
{

    private const string saveGameLocal = "C:\\Users\\Luthien\\Documents\\My Games\\Dream\\";

    private GameData gameData;

    private void Start()
    {
        gameData = GameData.Instance;

    }

    public void LoadGame(string name)
    {
        using(StreamReader sr = new StreamReader(saveGameLocal + name + ".json"))
        {
            string json = sr.ReadToEnd();
            gameData.gameDataStruct = JsonConvert.DeserializeObject<GameDataStruct>(json);
            foreach (itemStructForSaving itemLoad in gameData.gameDataStruct.items)
            {
                itemStruct item = new itemStruct();
                item.name = itemLoad.name;
                item.id = itemLoad.id;
                item.description = itemLoad.description;
                item.itemSprite = Resources.Load<Sprite>("Image\\"+itemLoad.itemSprite);
                item.video = Resources.Load<VideoClip>("Video\\"+itemLoad.video);
                Inventory.PickupItem(item);
            }
        }
    }

    public void SaveGame()
    {
        for(int i=0; i<Inventory.itemCount; i++)
        {
            itemStructForSaving saveItem = new itemStructForSaving();
            saveItem.id = Inventory.items[i].id;
            saveItem.name = Inventory.items[i].name;
            saveItem.description = Inventory.items[i].description;
            saveItem.itemSprite = Inventory.items[i].itemSprite.name;
            saveItem.video = Inventory.items[i].video.name;
            gameData.gameDataStruct.items.Add(saveItem);
        }

        using (StreamWriter sw = new StreamWriter(saveGameLocal + gameData.gameDataStruct.Name + ".json"))
        {
            sw.Write(JsonConvert.SerializeObject(gameData.gameDataStruct));
        }
        
    }

}
