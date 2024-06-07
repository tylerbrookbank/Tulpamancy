using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class InteractableFromItemCharacter : InteractableFromItem
{

    public int itemId2;
    public VideoClip itemInteractClip2;
    protected bool flipCharacter;
    protected bool isFlipped;

    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     protected void CheckShouldFlip()
    {
        if (GameData.Instance.gameDataStruct.drankShot && GameData.Instance.gameDataStruct.drankBeerCount == 2)
        {
            flipCharacter = true;
            Inventory.DropItem(Globals.UPSIDEDOWN_BOOK);
            itemStruct book = new itemStruct();
            book.id = Globals.BOOK;
            book.name = "Book";
            book.description = "Maybe I can read this now that I'm upside down too.";
            book.itemSprite = Resources.Load<Sprite>("Image\\book");
            book.video = Resources.Load<VideoClip>("Video\\openBook");
            Inventory.PickupItem(book);
        }
    }
    protected override void OnMouseDown()
    {
        if (!pointer.hasObject)
        {
            Debug.Log("Start Video");
            if (videoClip != null)
                playVideoEvent.Invoke(videoClip);
        }
        else
        {
            if (pointer.item.id == itemId)
            {
                customEventHandlerObject.useItemEvent.Invoke(pointer.item);
                customEventHandlerObject.letGoOfItemEvent.Invoke();
                playVideoEvent.Invoke(itemInteractClip);
            } else if(pointer.item.id == itemId2)
            {
                customEventHandlerObject.useItemEvent.Invoke(pointer.item);
                customEventHandlerObject.letGoOfItemEvent.Invoke();
                playVideoEvent.Invoke(itemInteractClip2);
            }
        }
        
    }

}
