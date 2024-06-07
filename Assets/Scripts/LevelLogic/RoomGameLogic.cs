using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

// for backgrounds
public enum RoomState
{
    lightOff,
    lightOn,
    table,
    menu,
    backToRoom,
    none,
    leavingRoom
}

// for items that require light
public enum LightState
{
    lightOn,
    lightOff
}

public struct BedroomEventStruct
{
    public RoomState roomState { get; set; }
    public RoomState lastState { get; set; }
    public LightState lightState { get; set; }
    public bool pickedUpBook { get; set; }
    public bool pickedUpCube { get; set; }
    public bool lookedAtBookFirstTime { get; set; }
    public bool readList { get; set; }
    public bool doorUnlocked { get; set; }
    public bool hasSeenGhost { get; set; }
}

public class RoomGameLogic : MonoBehaviour
{

    private ChangeRoomStateEvent changeRoomEvent;
    private ResetRoomStateEvent resetRoomStateEvent;
    private PickupEvent pickupEvent;
    private PlayAudioClip playAudioClip;
    private PlayAudioBackground playAudioBackground;
    private float startLeaveRoomTime;
    private bool playedOpening;

    public CustomEventHandler customEventHandlerObject;
    public BedroomEventStruct roomStateStruct;

    public SpriteRenderer background;
    public Sprite lightOffBackground;
    public Sprite lightOnBackground;
    public Sprite tableBackground;
    public Sprite tableNoBookBackground;
    public Sprite tableNoBookNoCubeBackground;
    public Sprite tableNoCubeBackground;
    public Sprite leaveRoomBackground;
    public GameSaveAndLoad saveLoader;
    public VideoClip EnterFromHallwayClip;

    public GameObject lightOff;
    public GameObject lightOn;
    public GameObject leaveTable1;
    public GameObject leaveTable2;
    public GameObject table;
    public GameObject door;
    public GameObject list;
    public GameObject bookObject;
    public GameObject cubeObject;
    public GameObject cursor;
    public GameObject HUD;

    // Start is called before the first frame update
    void Start()
    {
        playedOpening = false;
        changeRoomEvent = customEventHandlerObject.changeRoomStateEvent;
        resetRoomStateEvent = customEventHandlerObject.resetRoomStateEvent;
        pickupEvent = customEventHandlerObject.pickupEvent;
        playAudioClip = customEventHandlerObject.playAudioClip;
        playAudioBackground = customEventHandlerObject.playAudioBackground;
        pickupEvent.AddListener(PickUpItem);
        resetRoomStateEvent.AddListener(ResetRoomState);
        changeRoomEvent.AddListener(ChangeRoomState);
        roomStateStruct.roomState = RoomState.lightOff;
        roomStateStruct.lightState = LightState.lightOff;
        startLeaveRoomTime = 0;
        saveLoader.LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playedOpening)
        {
            playedOpening = true;
            customEventHandlerObject.playVideoEvent.Invoke(EnterFromHallwayClip);
        }
        CheckDoorState();
        CheckRoomState();

    }

    private void CheckRoomState()
    {
        switch (roomStateStruct.roomState)
        {
            case RoomState.lightOff:
                background.sprite = lightOffBackground;
                lightOff.SetActive(true);
                lightOn.SetActive(false);
                leaveTable1.SetActive(false);
                leaveTable2.SetActive(false);
                table.SetActive(true);
                door.SetActive(true);
                list.SetActive(true);
                bookObject.SetActive(false);
                cubeObject.SetActive(false);
                roomStateStruct.lightState = LightState.lightOff;
                break;
            case RoomState.lightOn:
                background.sprite = lightOnBackground;
                lightOff.SetActive(false);
                lightOn.SetActive(true);
                leaveTable1.SetActive(false);
                leaveTable2.SetActive(false);
                table.SetActive(true);
                door.SetActive(true);
                list.SetActive(true);
                bookObject.SetActive(false);
                cubeObject.SetActive(false);
                roomStateStruct.lightState = LightState.lightOn;
                break;
            case RoomState.table:
                
                if(roomStateStruct.pickedUpBook && !roomStateStruct.pickedUpCube)
                {
                    background.sprite = tableNoBookBackground;
                } else if(!roomStateStruct.pickedUpBook && roomStateStruct.pickedUpCube)
                {
                    background.sprite = tableNoCubeBackground;
                } else if(roomStateStruct.pickedUpBook && roomStateStruct.pickedUpCube)
                {
                    background.sprite = tableNoBookNoCubeBackground;
                }
                else
                {
                    background.sprite = tableBackground;
                }
                
                leaveTable1.SetActive(true);
                leaveTable2.SetActive(true);
                door.SetActive(false);
                table.SetActive(false);
                list.SetActive(false);
                if(!roomStateStruct.pickedUpCube)
                    cubeObject.SetActive(true);
                else
                    cubeObject.SetActive(false);
                if (!roomStateStruct.pickedUpBook)
                    bookObject.SetActive(true);
                else
                    bookObject.SetActive(false);
                if (lightOff.activeSelf)
                    lightOff.SetActive(false);
                if (lightOn.activeSelf)
                    lightOn.SetActive(false);
                break;
            case RoomState.menu:
                leaveTable1.SetActive(false);
                leaveTable2.SetActive(false);
                door.SetActive(false);
                table.SetActive(false);
                list.SetActive(false);
                bookObject.SetActive(false);
                if (lightOff.activeSelf)
                    lightOff.SetActive(false);
                if (lightOn.activeSelf)
                    lightOn.SetActive(false);
                break;
            case RoomState.leavingRoom:
                background.sprite = leaveRoomBackground;
                SceneManager.LoadScene("Hallway");
                break;
        }
    }

    public void CheckDoorState()
    {
        if(!roomStateStruct.doorUnlocked && roomStateStruct.lookedAtBookFirstTime && roomStateStruct.readList)
        {
            roomStateStruct.doorUnlocked = true;
            AudioClip clip = Resources.Load<AudioClip>("Audio/unlockDoor");
            playAudioClip.Invoke(clip);
        }
    }

    private void PickUpItem(itemStruct item)
    {
        switch (item.id)
        {
            case 0:
                roomStateStruct.pickedUpBook = true;
                break;
            case 1:
                roomStateStruct.lookedAtBookFirstTime = true;
                break;
            case 2:
                roomStateStruct.pickedUpCube = true;
                break;
        }
    }

    private void ResetRoomState()
    {
        Debug.Log("reset state");
        roomStateStruct.roomState = roomStateStruct.lastState;
    }

    private void ChangeRoomState(RoomState newState)
    {
        if(roomStateStruct.roomState != RoomState.menu)
            roomStateStruct.lastState = roomStateStruct.roomState;
        if(newState == RoomState.backToRoom)
        {
            roomStateStruct.roomState = (roomStateStruct.lightState == LightState.lightOn) ? RoomState.lightOn : RoomState.lightOff;
        }
        else
        {
            roomStateStruct.roomState = newState;
        }
    }

}
