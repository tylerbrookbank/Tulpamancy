using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEventHandler : MonoBehaviour
{

    public PlayVideoEvent playVideoEvent;
    public ChangeRoomStateEvent changeRoomStateEvent;
    public ResetRoomStateEvent resetRoomStateEvent;
    public UseItemEvent useItemEvent;
    public ShowItemInfoEvent ShowItemInfoEvent;
    public PlayAudioClip playAudioClip;
    public HoldItemEvent holdItemEvent;
    public LetGoOfItemEvent letGoOfItemEvent;
    public PlayAudioBackground playAudioBackground;
    public OnMouseHoveringObject onMouseHoveringObject;
    public ChangeKitchenStateEvent changeKitchenStateEvent;
    public TransitionDirectionEvent transitionDirection;
    public PickupEvent pickupEvent;

}
