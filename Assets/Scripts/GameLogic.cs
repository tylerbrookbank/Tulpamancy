using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameLogic : MonoBehaviour
{
    protected bool playedEnter;
    protected bool leaving;

    public CustomEventHandler customEventHandler;
    public GameSaveAndLoad gameSaveLoad;
    public VideoClip enterScene;
    private void Start()
    {
        BaseStart();
    }

    protected void BaseStart()
    {
        playedEnter = false;
    }

    protected virtual void UseItem(itemStruct item)
    {
        switch (item.id)
        {
            case Globals.BEER:
                Inventory.DropItem(item.id);
                break;
            case Globals.TEQUILLA_SHOT:
                Inventory.DropItem(item.id);
                break;
        }
    }

}
