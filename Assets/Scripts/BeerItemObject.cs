using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BeerItemObject : ItemObject
{

    public VideoClip pickupSecondBeer;
    bool pickedupFirst;

    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
        pickedupFirst = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnMouseDown()
    {
        GameData.Instance.gameDataStruct.kitchenGameState.beerCount++;
        if (!pointer.hasObject)
        {
            if (!pickedupFirst)
            {
                pickedupFirst = true;
                playVideoEvent.Invoke(pickUpVideo);
                Inventory.PickupItem(itemInfo);
                pickupEvent.Invoke(itemInfo);
            }
            else 
            {
                playVideoEvent.Invoke(pickupSecondBeer);
                pickupEvent.Invoke(itemInfo);
                Inventory.PickupItem(itemInfo);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

}
