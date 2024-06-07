using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipController : MonoBehaviour
{


    private PlayAudioClip playAudioClip;
    private PlayAudioBackground playAudioBackground;
    private AudioSource audioSource;

    private bool playUpsideDownAudio;
    private float time;

    public AudioSource backgroundAudio;
    public CustomEventHandler customEventHandler;

    // Start is called before the first frame update
    void Start()
    {
        playAudioClip = customEventHandler.playAudioClip;
        playAudioBackground = customEventHandler.playAudioBackground;
        audioSource = gameObject.GetComponent<AudioSource>();
        playAudioBackground.AddListener(PlayAudioBack);
        playAudioClip.AddListener(PlayAudio);
    }

    // Update is called once per frame
    void Update()
    {
        if(playUpsideDownAudio && (Time.time - time) > 3)
        {
            audioSource.Play();
            playUpsideDownAudio = false;
        }
    }

    private void PlayAudioBack(AudioClip audio)
    {
        backgroundAudio.Pause();
        backgroundAudio.clip = audio;
        backgroundAudio.Play();
    }

    private void PlayAudio(AudioClip audio)
    {
        Debug.Log("Playing " + audio.name);

        if(audio.name.Equals("upsideDownBookAudio"))
        {
            playUpsideDownAudio = true;
            time = Time.time;
            audioSource.clip = audio;
        } else
        {
            audioSource.clip = audio;
            audioSource.Play();
        }
    }

}
