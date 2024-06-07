using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class InteractableFromItem : Interactable
{

    public int itemId;
    public VideoClip itemInteractClip;

    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
    }

    // Update is called once per frame
    void Update()
    {
        
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
                gameObject.SetActive(false);
            }
        }
        
    }

}
