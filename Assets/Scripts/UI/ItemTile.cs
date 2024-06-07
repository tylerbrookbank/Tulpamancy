using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTile : MonoBehaviour
{

    // 241, 0, 0, 51
    private ShowItemInfoEvent showItemInfoEvent;
    private PlayVideoEvent playVideoEvent;
    private HoldItemEvent holdItemEvent;

    public CustomEventHandler customEventHandler;
    public itemStruct item;

    private void Start()
    {
        showItemInfoEvent = customEventHandler.GetComponent<CustomEventHandler>().ShowItemInfoEvent;
        playVideoEvent = customEventHandler.GetComponent<CustomEventHandler>().playVideoEvent;
        holdItemEvent = customEventHandler.GetComponent<CustomEventHandler>().holdItemEvent;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            OnRightMouseDown();
        }
    }

    private void OnMouseExit()
    {
        SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer>();
        rend.color = new Color(241, 0, 0, 51);
    }

    private void OnMouseOver()
    {
        SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer>();
        rend.color = new Color(241, 0, 241, 51);
        showItemInfoEvent.Invoke(item);
    }
    private void OnMouseDown()
    {
        holdItemEvent.Invoke(item);
        transform.parent.GetComponent<InventorySlots>().menuButton.ManualClick();
    }

    private void OnRightMouseDown()
    {
        BoxCollider2D boxCollider2D = gameObject.GetComponent<BoxCollider2D>();

        if (boxCollider2D.IsTouchingLayers())
        {
            Debug.Log("Play Video");
            playVideoEvent.Invoke(item.video);
        }
    }

}
