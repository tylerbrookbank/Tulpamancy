using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RoomInteractable : Interactable
{

    public RoomState roomStateChange;

    private void Start()
    {
        BaseStart();
    }

    protected override void OnMouseDown()
    {
        if (!pointer.hasObject)
        {
            Debug.Log("Start Video");
            if (videoClip != null)
                playVideoEvent.Invoke(videoClip);
            if (roomStateChange != RoomState.none)
                changeRoomStateEvent.Invoke(roomStateChange);
        }
    }

}
