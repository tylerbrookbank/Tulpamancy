using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ScreenTransition : Interactable
{

    public TransitionDirection transitionDirection;
    public ScreenID transitionDestination;

    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
    }

    protected override void OnMouseOver()
    {
        base.OnMouseOver();
        customEventHandlerObject.transitionDirection.Invoke(transitionDirection);
    }

    protected override void OnMouseDown()
    {
        if (!pointer.hasObject)
        {
            Debug.Log("Start Video");
            if (videoClip != null)
                playVideoEvent.Invoke(videoClip);
            // if video add lag?
            switch(transitionDestination)
            {
                case ScreenID.ApartmentFrontDoor:
                    SceneManager.LoadScene("FrontDoorApartment");
                    break;
                case ScreenID.ApartmentHallway:
                    SceneManager.LoadScene("Hallway");
                    break;
                case ScreenID.Bedroom:
                    SceneManager.LoadScene("Bedroom");
                    break;
                case ScreenID.Kitchen:
                    SceneManager.LoadScene("Kitchen");
                    break;
            }
        }
    }

}
