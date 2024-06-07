using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoControllerKitchen : MonoBehaviour
{

    public VideoPlayer video;
    public GameObject customEventHandlerObject;

    private PlayVideoEvent playVideoEvent;
    private PlayAudioClip playAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        playVideoEvent = customEventHandlerObject.GetComponent<CustomEventHandler>().playVideoEvent;
        playAudioClip = customEventHandlerObject.GetComponent<CustomEventHandler>().playAudioClip;
        playVideoEvent.AddListener(PlayVideo);
    }

    // Update is called once per frame
    void Update()
    {
        if (video.isPaused)
        {
            video.Stop();
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
        }
    }

    //  should this be in roo, logic object??
    private bool CanPlayClip(VideoClip video)
    {
        bool retVal = true;

        return retVal;
    }

}
