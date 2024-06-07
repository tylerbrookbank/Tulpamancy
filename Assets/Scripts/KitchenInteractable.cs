using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class KitchenInteractable : Interactable
{

    public KitchenGameLogic kitchenGameLogic;
    public KitchenState kitchenStateChange;

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
            if (kitchenStateChange != KitchenState.none)
                customEventHandlerObject.changeKitchenStateEvent.Invoke(kitchenStateChange);
        }
    }

}
