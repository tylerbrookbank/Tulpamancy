using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class PlayerController : InteractableFromItemCharacter
{

    private SpriteRenderer sRend;
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
        CheckShouldFlip();
        if (flipCharacter && !isFlipped)
        {
            isFlipped = true;
            FlipCharacter();
        }
        SetCharacterPose();
        if (GameData.Instance.gameDataStruct.bedroomEventStruct.roomState == RoomState.table || GameData.Instance.gameDataStruct.bedroomEventStruct.roomState == RoomState.menu || GameData.Instance.gameDataStruct.bedroomEventStruct.roomState == RoomState.leavingRoom)
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
    private void FlipCharacter()
    {
        Vector3 newPos = new Vector3(-2.07f, 0.57f, -2);
        transform.Rotate(180, 0, 0);
        transform.position = newPos;
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
