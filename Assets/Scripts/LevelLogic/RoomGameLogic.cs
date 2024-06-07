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
    lightOff,
    lightOn
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

public class RoomGameLogic : GameLogic
{

    private ChangeRoomStateEvent changeRoomEvent;
    private ResetRoomStateEvent resetRoomStateEvent;
    private PickupEvent pickupEvent;
    private PlayAudioClip playAudioClip;
    private PlayAudioBackground playAudioBackground;
    private float startLeaveRoomTime;
    private bool playedOpening;

    public SpriteRenderer background;
    public Sprite lightOffBackground;
    public Sprite lightOnBackground;
    public Sprite tableBackground;
    public Sprite tableNoBookBackground;
    public Sprite tableNoBookNoCubeBackground;
    public Sprite tableNoCubeBackground;
    public Sprite leaveRoomBackground;
    public VideoClip EnterFromHallwayClip;
    public VideoPlayer vp;

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
        changeRoomEvent = customEventHandler.changeRoomStateEvent;
        resetRoomStateEvent = customEventHandler.resetRoomStateEvent;
        pickupEvent = customEventHandler.pickupEvent;
        playAudioClip = customEventHandler.playAudioClip;
        playAudioBackground = customEventHandler.playAudioBackground;
        pickupEvent.AddListener(PickUpItem);
        resetRoomStateEvent.AddListener(ResetRoomState);
        changeRoomEvent.AddListener(ChangeRoomState);
        startLeaveRoomTime = 0;
        GameData.Instance.gameDataStruct.screenID = ScreenID.Bedroom;
        switch(GameData.Instance.gameDataStruct.bedroomEventStruct.lightState)
        {
            case LightState.lightOff:
                GameData.Instance.gameDataStruct.bedroomEventStruct.roomState = RoomState.lightOff;
            break;
            case LightState.lightOn:
                GameData.Instance.gameDataStruct.bedroomEventStruct.roomState = RoomState.lightOn;
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameState();
        if (!playedOpening)
        {
            playedOpening = true;
            customEventHandler.playVideoEvent.Invoke(EnterFromHallwayClip);
        }
        CheckRoomState();

    }

    private void CheckRoomState()
    {
        switch (GameData.Instance.gameDataStruct.bedroomEventStruct.roomState)
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
                GameData.Instance.gameDataStruct.bedroomEventStruct.lightState = LightState.lightOff;
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
                GameData.Instance.gameDataStruct.bedroomEventStruct.lightState = LightState.lightOn;
                break;
            case RoomState.table:
                
                if(GameData.Instance.gameDataStruct.bedroomEventStruct.pickedUpBook && !GameData.Instance.gameDataStruct.bedroomEventStruct.pickedUpCube)
                {
                    background.sprite = tableNoBookBackground;
                } else if(!GameData.Instance.gameDataStruct.bedroomEventStruct.pickedUpBook && GameData.Instance.gameDataStruct.bedroomEventStruct.pickedUpCube)
                {
                    background.sprite = tableNoCubeBackground;
                } else if(GameData.Instance.gameDataStruct.bedroomEventStruct.pickedUpBook && GameData.Instance.gameDataStruct.bedroomEventStruct.pickedUpCube)
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
                if(!GameData.Instance.gameDataStruct.bedroomEventStruct.pickedUpCube)
                    cubeObject.SetActive(true);
                else
                    cubeObject.SetActive(false);
                if (!GameData.Instance.gameDataStruct.bedroomEventStruct.pickedUpBook)
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
        if(!GameData.Instance.gameDataStruct.bedroomEventStruct.doorUnlocked && GameData.Instance.gameDataStruct.bedroomEventStruct.lookedAtBookFirstTime && GameData.Instance.gameDataStruct.bedroomEventStruct.readList && !vp.isPlaying)
        {
            GameData.Instance.gameDataStruct.bedroomEventStruct.doorUnlocked = true;
            AudioClip clip = Resources.Load<AudioClip>("Audio/unlockDoor");
            playAudioClip.Invoke(clip);
        }
    }

    private void PickUpItem(itemStruct item)
    {
        switch (item.id)
        {
            case 0:
                GameData.Instance.gameDataStruct.bedroomEventStruct.pickedUpBook = true;
                break;
            case 1:
                GameData.Instance.gameDataStruct.bedroomEventStruct.lookedAtBookFirstTime = true;
                break;
            case 2:
                GameData.Instance.gameDataStruct.bedroomEventStruct.pickedUpCube = true;
                break;
        }
    }

    private void ResetRoomState()
    {
        Debug.Log("reset state");
        GameData.Instance.gameDataStruct.bedroomEventStruct.roomState = GameData.Instance.gameDataStruct.bedroomEventStruct.lastState;
    }

    private void ChangeRoomState(RoomState newState)
    {
        if(GameData.Instance.gameDataStruct.bedroomEventStruct.roomState != RoomState.menu)
            GameData.Instance.gameDataStruct.bedroomEventStruct.lastState = GameData.Instance.gameDataStruct.bedroomEventStruct.roomState;
        if(newState == RoomState.backToRoom)
        {
            GameData.Instance.gameDataStruct.bedroomEventStruct.roomState = (GameData.Instance.gameDataStruct.bedroomEventStruct.lightState == LightState.lightOn) ? RoomState.lightOn : RoomState.lightOff;
        }
        else
        {
            GameData.Instance.gameDataStruct.bedroomEventStruct.roomState = newState;
        }
    }

}
