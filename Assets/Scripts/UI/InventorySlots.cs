using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySlots : MonoBehaviour
{
    private ShowItemInfoEvent showItemInfoEvent;

    private List<SpriteRenderer> slots;

    public GameObject customEventHandlerObject;
    public HUDObject menuButton;

    public TextMeshPro itemTitle;
    public TextMeshPro itemDescription;
    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        showItemInfoEvent = customEventHandlerObject.GetComponent<CustomEventHandler>().ShowItemInfoEvent;
        showItemInfoEvent.AddListener(ShowItemInfo);
        slots = new List<SpriteRenderer>();
        itemTitle.text = "";
        itemDescription.text = "";
        SetupSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
    }

    private void SetupSlots()
    {
        for(int i=0; i<transform.childCount; i++)
        {
            slots.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
        }
    }

    private void UpdateSlots()
    {
        if(Inventory.itemCount > 0)
        {
            for(int i=0; i< transform.childCount; i++)
            {
                if(i < Inventory.itemCount) 
                {
                    slots[i].gameObject.SetActive(true);
                    ItemTile tile = slots[i].gameObject.GetComponent<ItemTile>();
                    tile.item = Inventory.items[i];
                    SpriteRenderer rend = slots[i].transform.GetChild(0).GetComponent<SpriteRenderer>();
                    rend.sprite = Inventory.items[i].itemSprite;
                } 
                else
                {
                    slots[i].gameObject.SetActive(false);
                }
                
            }
        }
    }

    private void ShowItemInfo(itemStruct item)
    {
        itemTitle.text = item.name;
        itemDescription.text = item.description;
    }

}
