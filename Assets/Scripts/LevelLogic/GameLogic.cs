using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameLogic : MonoBehaviour
{
    protected bool playedEnter;
    protected bool leaving;
    protected bool canExit;
    protected float timer;

    public CustomEventHandler customEventHandler;
    public VideoClip enterScene;

    private void Start()
    {
        BaseStart();
    }

    protected void BaseStart()
    {
        playedEnter = false;
        customEventHandler.playVideoEvent.AddListener(CheckPlayVideo);
        customEventHandler.useItemEvent.AddListener(UseItem);
    }

    protected void CheckGameState()
    {
        if (canExit && timer > 5)
        {
            Application.Quit();
        } else if(canExit)
        {
            timer += Time.deltaTime;
        }
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

    protected void CheckPlayVideo(VideoClip video)
    {
        if (video.name.Equals("DrinkBeer"))
            GameData.Instance.gameDataStruct.drankBeerCount++;
        else if (video.name.Equals("drinkShot"))
            GameData.Instance.gameDataStruct.drankShot = true;
        else if (video.name.Equals("openBook"))
        {
            canExit = true;
            timer = Time.deltaTime;
        }
    }

}
