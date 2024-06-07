using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDObject : MonoBehaviour
{

    protected SpriteRenderer sRenderer;
    protected ResetRoomStateEvent resetRoomStateEvent;
    protected ChangeRoomStateEvent changeRoomStateEvent;
    protected LetGoOfItemEvent letGoOfItemEvent;

    protected bool clicked;

    public Sprite inactiveIcon;
    public Sprite activeIcon;

    public GameObject customEventHandlerObject;
    public GameObject controlObject;

    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
    }

    protected void BaseStart()
    {
        sRenderer = gameObject.GetComponent<SpriteRenderer>();
        resetRoomStateEvent = customEventHandlerObject.GetComponent<CustomEventHandler>().resetRoomStateEvent;
        changeRoomStateEvent = customEventHandlerObject.GetComponent<CustomEventHandler>().changeRoomStateEvent;
        letGoOfItemEvent = customEventHandlerObject.GetComponent<CustomEventHandler>().letGoOfItemEvent;
        sRenderer.sprite = inactiveIcon;
        clicked = false;
        //controlObject.SetActive(true);
        //controlObject.SetActive(false);
    }

    protected virtual void OnMouseDown()
    {
        letGoOfItemEvent.Invoke();
        SetGameState();
        clicked = !clicked;
    }

    protected void OnMouseOver()
    {
        if(!clicked)
            sRenderer.sprite = activeIcon;
    }

    protected void OnMouseExit()
    {
        if(!clicked)
            sRenderer.sprite = inactiveIcon;
    }

    protected void SetGameState()
    {
        if (!clicked)
        {
            changeRoomStateEvent.Invoke(RoomState.menu);
            controlObject.SetActive(true);
        } else
        {
            resetRoomStateEvent.Invoke();
            controlObject.SetActive(false);
        }
    }

    public void ManualClick()
    {
        SetGameState();
        sRenderer.sprite = inactiveIcon;
        clicked = !clicked;
    }

}
