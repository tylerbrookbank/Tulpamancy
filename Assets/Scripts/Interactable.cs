using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Interactable : MonoBehaviour
{

    protected PlayVideoEvent playVideoEvent;
    protected ChangeRoomStateEvent changeRoomStateEvent;
    protected OnMouseHoveringObject onMouseHoveringObject;

    public VideoClip videoClip;
    public CustomEventHandler customEventHandlerObject;
    public CursorPointer pointer;
    public int objectId;

    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void BaseStart()
    {
        playVideoEvent = customEventHandlerObject.playVideoEvent;
        changeRoomStateEvent = customEventHandlerObject.changeRoomStateEvent;
        onMouseHoveringObject = customEventHandlerObject.onMouseHoveringObject;
    }

    protected virtual void OnMouseOver()
    {
        if(onMouseHoveringObject != null)
            onMouseHoveringObject.Invoke(objectId);
    }

    protected virtual void OnMouseDown()
    {
        if (!pointer.hasObject)
        {
            Debug.Log("Start Video");
            if (videoClip != null)
                playVideoEvent.Invoke(videoClip);
        }
    }

}
