using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorPointer : MonoBehaviour
{

    private SpriteRenderer current;
    private BoxCollider2D collider2d;
    private TransitionDirection transitionDirection;
    private bool rotated;
    private Vector3 resetRotation;
    public bool hasObject { get; private set; }
    public itemStruct item { get; private set; }
    private HoldItemEvent holdItemEvent;
    private LetGoOfItemEvent letGoOfItemEvent;

    public Sprite pointer;
    public Sprite grab;
    public Sprite examine;
    public CustomEventHandler customEventHandler;

    // Start is called before the first frame update
    void Start()
    {
        current = this.GetComponent<SpriteRenderer>();
        collider2d = this.GetComponent<BoxCollider2D>();
        holdItemEvent = customEventHandler.holdItemEvent;
        letGoOfItemEvent = customEventHandler.letGoOfItemEvent;
        customEventHandler.transitionDirection.AddListener(GetDirection);
        holdItemEvent.AddListener(GrabItem);
        letGoOfItemEvent.AddListener(LetGoOfItem);
        rotated = false;
        current.sprite = pointer;
    }

    // Update is called once per frame
    void Update()
    {
        SetCursorSprite();
        SetCursorPosition();
    }

    private void SetCursorSprite()
    {
        current.sprite = pointer;
        current.color = new Color(255, 255, 255);
        gameObject.GetComponent<Animator>().enabled = false;
        if (hasObject)
        {
            current.sprite = item.itemSprite;
            if (collider2d.IsTouchingLayers())
            {
                current.color = new Color(255, 0, 0);
            }
        }
        else
        {
            if (collider2d.IsTouchingLayers(64))
            {
                current.sprite = examine;
            }
            else if (collider2d.IsTouchingLayers(384))
            {
                current.sprite = grab;
            } else if(collider2d.IsTouchingLayers(512))
            {
                gameObject.GetComponent<Animator>().enabled = true;
                
            } else
            {
                if(current.flipX)
                {
                    current.flipX = false;
                }
                if (rotated)
                {
                    transform.Rotate(resetRotation);
                    rotated = false;
                }
            }
        }
    }

    private void GetDirection(TransitionDirection direction)
    {
        if (!rotated)
        {
            switch (direction)
            {
                case TransitionDirection.Backward:
                    transform.Rotate(0, 0, 180);
                    resetRotation = new Vector3(0, 0, -180);
                    break;
                case TransitionDirection.Left:
                    transform.Rotate(0, 0, 90);
                    resetRotation = new Vector3(0, 0, -90);
                    break;
                case TransitionDirection.Right:
                    transform.Rotate(0, 0, -90);
                    resetRotation = new Vector3(0, 0, 90);
                    current.flipX = true;
                    break;
            }
            rotated = true;
        }
    }

    private void SetCursorPosition()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = -4;
        transform.position = mouse;
    }

    private void LetGoOfItem()
    {
        hasObject = false;
    }

    private void GrabItem(itemStruct itemToHold)
    {
        hasObject = true;
        item = itemToHold;
    }

}
