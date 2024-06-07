using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    private SpriteRenderer sRend;
    private PlayVideoEvent playVideoEvent;
    private RoomGameLogic roomLogic;
    private BoxCollider2D playerCollider2D;

    public Sprite Idle;
    public Sprite LookLeft;
    public Sprite LookRight;
    public Sprite LookBack;
    public Sprite LookDown;

    public VideoClip examineSelfLightOff;
    public VideoClip examineSelfLightOn;
    public GameObject roomLogicObject;
    public GameObject customEventHandlerObject;
    public CursorPointer pointer;

    // Start is called before the first frame update
    void Start()
    {
        sRend = this.gameObject.GetComponent<SpriteRenderer>();
        sRend.sprite = Idle;
        Cursor.visible = false;
        playVideoEvent = customEventHandlerObject.GetComponent<CustomEventHandler>().playVideoEvent;
        roomLogic = roomLogicObject.GetComponent<RoomGameLogic>();
        playerCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SetCharacterPose();
        if (roomLogic.roomStateStruct.roomState == RoomState.table || roomLogic.roomStateStruct.roomState == RoomState.menu || roomLogic.roomStateStruct.roomState == RoomState.leavingRoom)
        {
            playerCollider2D.enabled = false;
            sRend.enabled = false;
        }
        else
        {
            playerCollider2D.enabled = true;
            sRend.enabled = true;
        }
    }

    void OnMouseDown()
    {
        if (!pointer.hasObject)
        {
            Debug.Log("Start video");
            if (roomLogic.roomStateStruct.roomState == RoomState.lightOff)
                playVideoEvent.Invoke(examineSelfLightOff);
            else
                playVideoEvent.Invoke(examineSelfLightOn);
        }
    }

    private void SetCharacterPose()
    {
        bool lookingDown = false;
        Vector2 mouseLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 characterLoc = transform.position;

        lookingDown = (mouseLoc.y - characterLoc.y < 0);

        sRend.sprite = Idle;
        sRend.flipX = false;
        sRend.enabled = true;
        if (mouseLoc.x - characterLoc.x < -1)
        {
            sRend.sprite = lookingDown ? LookDown : LookLeft;
            sRend.flipX = lookingDown ? true : false;
        } else if (mouseLoc.x - characterLoc.x > 1)
        {
            sRend.sprite = lookingDown ? LookDown : LookRight;
        }

    }
}
