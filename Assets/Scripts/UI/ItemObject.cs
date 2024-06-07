using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ItemObject : MonoBehaviour
{

    protected PickupEvent pickupEvent;
    protected PlayVideoEvent playVideoEvent;
    protected itemStruct itemInfo;

    public GameObject customEventHandler;
    public Sprite itemSprite;
    public VideoClip pickUpVideo;
    public VideoClip inventoryVideo;
    public string itemName;
    public string itemDescription;
    public int id;
    public CursorPointer pointer;

    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
    }

    protected void BaseStart()
    {
        pickupEvent = customEventHandler.GetComponent<CustomEventHandler>().pickupEvent;
        playVideoEvent = customEventHandler.GetComponent<CustomEventHandler>().playVideoEvent;
        itemInfo.name = itemName;
        itemInfo.description = itemDescription;
        itemInfo.itemSprite = itemSprite;
        itemInfo.video = inventoryVideo;
        itemInfo.id = id;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnMouseDown()
    {
        if (!pointer.hasObject)
        {
            playVideoEvent.Invoke(pickUpVideo);
            pickupEvent.Invoke(itemInfo);
            Inventory.PickupItem(itemInfo);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
