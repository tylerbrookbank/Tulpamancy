using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public enum KitchenState
{
    Counter,
    Fridge,
    none,
    backToRoom
}

public struct KitchenGameState
{
    public KitchenState state;
    public short beerCount;
    public bool pickedUpTaq;
    public bool pouredTaq;
}

public class KitchenGameLogic : GameLogic
{

    protected const short MAX_BEER_NUM = 3;

    public Sprite counterBackground;
    public Sprite fridgeAllBackground;
    public Sprite fridgeNoTaqBackground;
    public Sprite fridgeNoTaq4BeerBackground;
    public Sprite fridgeNoTaq3BeerBackground;
    public Sprite fridge4BeerBackground;
    public Sprite fridge3BeerBackground;

    public GameObject shotGlass;
    public GameObject fridge;
    public GameObject beer;
    public GameObject tequilla;
    public GameObject backToCounter;

    public SpriteRenderer backgroundRenderer;

    public VideoPlayer videoPlayer;
    public KitchenGameState kitchenGameState;

    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
        kitchenGameState = new KitchenGameState();
        customEventHandler.changeKitchenStateEvent.AddListener(ChangeState);
        customEventHandler.pickupEvent.AddListener(CheckPickedUpItem);
        customEventHandler.resetRoomStateEvent.AddListener(ResetToCounter);
        customEventHandler.useItemEvent.AddListener(UseItem);
        kitchenGameState.state = KitchenState.Counter;
        gameSaveLoad.LoadGame();
    }

    // Update is called once per frame
    void Update()
    {

        if(leaving && videoPlayer.isPaused)
        {
            //saveLoader.SaveGame();
            SceneManager.LoadScene("Bedroom");
        }
        else
        {
            if (!playedEnter)
            {
                customEventHandler.playVideoEvent.Invoke(enterScene); 
                playedEnter = true;
            }

            if (kitchenGameState.state == KitchenState.backToRoom)
            {
                leaving = true;
            }

            CheckState();
        }
    }

    protected void CheckState()
    {

        switch(kitchenGameState.state)
        {
            case KitchenState.Counter:
                backgroundRenderer.sprite = counterBackground;
                tequilla.SetActive(false);
                beer.SetActive(false);
                backToCounter.SetActive(false);
                shotGlass.SetActive(true);
                fridge.SetActive(true);
                break;
            case KitchenState.Fridge:
                if(kitchenGameState.pickedUpTaq)
                {
                    switch (kitchenGameState.beerCount)
                    {
                        case 0:
                            backgroundRenderer.sprite = fridgeNoTaqBackground;
                            break;
                        case 1:
                            backgroundRenderer.sprite = fridgeNoTaq4BeerBackground;
                            break;
                        case 2:
                            backgroundRenderer.sprite = fridgeNoTaq3BeerBackground;
                            break;
                    }
                } else
                {
                    switch (kitchenGameState.beerCount)
                    {
                        case 0:
                            backgroundRenderer.sprite = fridgeAllBackground;
                            break;
                        case 1:
                            backgroundRenderer.sprite = fridge4BeerBackground;
                            break;
                        case 2:
                            backgroundRenderer.sprite = fridge3BeerBackground;
                            break;
                    }
                }
                tequilla.SetActive(true);
                beer.SetActive(true);
                backToCounter.SetActive(true);
                shotGlass.SetActive(false);
                fridge.SetActive(false);
                break;
        }
    }

    protected override void UseItem(itemStruct item)
    {
        base.UseItem(item);
        switch (item.id)
        {
            case Globals.TEQUILLA:
                Inventory.DropItem(item.id);
                itemStruct shotGlass = new itemStruct();
                shotGlass.id = Globals.TEQUILLA_SHOT;
                shotGlass.name = "Tequilla Shot";
                shotGlass.description = "Oh boy imma get drunk";
                shotGlass.itemSprite = Resources.Load<Sprite>("Image\\taq");
                shotGlass.video = Resources.Load<VideoClip>("pickupTaq");
                Inventory.PickupItem(shotGlass);
                kitchenGameState.pouredTaq = true;
                break;
        }
    }

    protected void ResetToCounter()
    {
        kitchenGameState.state = KitchenState.Counter;
    }

    protected void CheckPickedUpItem(itemStruct item)
    {
        if(item.id == Globals.TEQUILLA)
        {
            kitchenGameState.pickedUpTaq = true;
        }
    }

    protected void ChangeState(KitchenState state)
    {
        kitchenGameState.state = state;
    }

}
