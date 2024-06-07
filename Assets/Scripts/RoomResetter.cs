using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomResetter : MonoBehaviour
{

    private ResetRoomStateEvent resetRoomStateEvent;

    public GameObject customEventHandlerObject;

    // Start is called before the first frame update
    void Start()
    {
        resetRoomStateEvent = customEventHandlerObject.GetComponent<CustomEventHandler>().resetRoomStateEvent;
    }

    private void OnMouseDown()
    {
        resetRoomStateEvent.Invoke();
    }

}
