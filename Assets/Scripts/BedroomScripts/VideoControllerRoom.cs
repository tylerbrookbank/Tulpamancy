using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{

    public VideoPlayer video;
    public GameObject customEventHandlerObject;
    public RoomGameLogic roomLogic;

    private PlayVideoEvent playVideoEvent;
    private PlayAudioClip playAudioClip;
    private ChangeRoomStateEvent changeRoomStateEvent;

    // Start is called before the first frame update
    void Start()
    {
        playVideoEvent = customEventHandlerObject.GetComponent<CustomEventHandler>().playVideoEvent;
        playAudioClip = customEventHandlerObject.GetComponent<CustomEventHandler>().playAudioClip;
        changeRoomStateEvent = customEventHandlerObject.GetComponent<CustomEventHandler>().changeRoomStateEvent;
        playVideoEvent.AddListener(PlayVideo);
    }

    // Update is called once per frame
    void Update()
    {
        if (video.isPaused)
        {
            video.Stop();
            roomLogic.CheckDoorState();
            if (video.clip.name.Equals("LeaveRoom"))
            {
                changeRoomStateEvent.Invoke(RoomState.leavingRoom);
            }
        }
    }

    private void PlayVideo(VideoClip videoClip)
    {
        Debug.Log("Starting video");
        if (!video.isPlaying && CanPlayClip(videoClip))
        {
            video.clip = videoClip;
            video.Play();

            Debug.Log("Playing " + videoClip.name);
            if (videoClip.name.Equals("OpenBookUpsideDown"))
            {
                // 0 id for book
                Inventory.DropItem(Globals.WEIRD_BOOK);
                itemStruct item = new itemStruct();
                item.name = "Upside Down Book";
                item.description = "No matter how much I turn this book its always upside down. Wish I could find a way to read it";
                item.id = 1;
                item.itemSprite = Resources.Load<Sprite>("book");
                item.video = Resources.Load<VideoClip>("OpenBookUpsideDown");
                Inventory.PickupItem(item);
                customEventHandlerObject.GetComponent<CustomEventHandler>().pickupEvent.Invoke(item);
                AudioClip upsideDown = Resources.Load<AudioClip>("Audio/upsideDownBookAudio");
                playAudioClip.Invoke(upsideDown);
            }
            else if (videoClip.name.Equals("CheckList"))
            {
                GameData.Instance.gameDataStruct.bedroomEventStruct.readList = true;
            }
        }
    }

    //  should this be in roo, logic object??
    private bool CanPlayClip(VideoClip video)
    {
        bool retVal = true;

        if((video.name.Equals("OpenBookUpsideDown") || video.name.Equals("CheckList")) && GameData.Instance.gameDataStruct.bedroomEventStruct.lightState == LightState.lightOff)
        {
            // send event its too dark to read...
            AudioClip tooDark = Resources.Load<AudioClip>("Audio/toodark");
            playAudioClip.Invoke(tooDark);
            retVal = false;
        } else if(video.name.Equals("LeaveRoom") && (!GameData.Instance.gameDataStruct.bedroomEventStruct.lookedAtBookFirstTime || !GameData.Instance.gameDataStruct.bedroomEventStruct.readList))
        {
            AudioClip locked = Resources.Load<AudioClip>("Audio/doorLocked");
            playAudioClip.Invoke(locked);
            retVal = false;
        }

        return retVal;
    }

}
