using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory Instance;

    private const int INV_SIZE = 6;

    public static itemStruct[] items = new itemStruct[INV_SIZE];
    public static int itemCount;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void DropItem(int id)
    {
        itemStruct[] newItems = new itemStruct[INV_SIZE];
        int j = 0;
        bool dropped = false;
        for (int i = 0; i < itemCount; i++)
        {
            if (items[i].id != id || dropped)
            {
                newItems[j++] = items[i];
            }
            else
            {
                dropped = true;
            }
        }
        items = newItems;
        itemCount--;
    }

    public static void PickupItem(itemStruct item)
    {
        if (items == null)
            items = new itemStruct[INV_SIZE];
        if (itemCount < 6)
        {
            Debug.Log("Picked up " + item.name);
            items[itemCount++] = item;
        }
    }

}
