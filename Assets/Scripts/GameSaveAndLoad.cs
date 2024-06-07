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

    public RoomGameLogic room;

    private void Start()
    {

    }

    public void LoadGame()
    {
        using(StreamReader sr = new StreamReader(saveGameLocal + "inventory.json"))
        {
            // only try to load if no items
            // TODO
            if(Inventory.itemCount == 0)
            {
                string json = sr.ReadToEnd();
                List<itemStructForSaving> inv = JsonConvert.DeserializeObject<List<itemStructForSaving>>(json);
                foreach (itemStructForSaving itemLoad in inv)
                {
                    itemStruct item = new itemStruct();
                    item.name = itemLoad.name;
                    item.description = itemLoad.description;
                    item.itemSprite = Resources.Load<Sprite>("Image/" + itemLoad.itemSprite);
                    item.video = Resources.Load<VideoClip>("Video/" + itemLoad.video);
                    item.id = itemLoad.id;
                    Inventory.PickupItem(item);
                }
            }
        }

        // load bedroom
        if (room != null)
        {
            using (StreamReader sr = new StreamReader(saveGameLocal + "roomState.json"))
            {
                string json = sr.ReadToEnd();
                BedroomEventStruct roomSave = JsonConvert.DeserializeObject<BedroomEventStruct>(json);
                room.roomStateStruct = roomSave;
                switch (room.roomStateStruct.lightState)
                {
                    case LightState.lightOff:
                        room.roomStateStruct.roomState = RoomState.lightOff;
                        break;
                    case LightState.lightOn:
                        room.roomStateStruct.roomState = RoomState.lightOn;
                        break;
                }
            }
        }
    }

    public void SaveGame()
    {
        List<itemStructForSaving> saveInv = new List<itemStructForSaving>();

        foreach (itemStruct item in Inventory.items)
        {
            if (item.name != null)
            {
                itemStructForSaving saveItem = new itemStructForSaving();
                saveItem.name = item.name;
                saveItem.description = item.description;
                saveItem.itemSprite = item.itemSprite.name;
                saveItem.video = item.video.name;
                saveItem.id = item.id;
                saveInv.Add(saveItem);
            }
        }

        using (StreamWriter sr = new StreamWriter(saveGameLocal + "inventory.json"))
        {
            sr.Write(JsonConvert.SerializeObject(saveInv));
        }

        if(room != null)
        {
            using (StreamWriter sr = new StreamWriter(saveGameLocal + "roomState.json"))
            {
                sr.Write(JsonConvert.SerializeObject(room.roomStateStruct));
            }
        }
        
    }

}
