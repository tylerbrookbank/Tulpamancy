using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenCharacter : InteractableFromItemCharacter
{

    private SpriteRenderer sRend;
    private BoxCollider2D cursorCollider2d;
    private BoxCollider2D characterCollider2d;

    public Sprite idleSprite;
    public Sprite lookAtShotGlassSprite;
    public Sprite lookAtFridge;
    public KitchenGameLogic kitchenGameLogic;

    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
        onMouseHoveringObject.AddListener(HoveringOverObject);
        sRend = gameObject.GetComponent<SpriteRenderer>();
        cursorCollider2d = pointer.gameObject.GetComponent<BoxCollider2D>();
        characterCollider2d = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!cursorCollider2d.IsTouchingLayers())
        {
            sRend.sprite = idleSprite;
        }

        if (kitchenGameLogic.kitchenGameState.state == KitchenState.Counter)
        {
            sRend.enabled = true;
            characterCollider2d.enabled = true;
        } else
        {
            sRend.enabled = false;
            characterCollider2d.enabled = false;
        }

    }

    private void HoveringOverObject(int itemId)
    {
        switch (itemId)
        {
            case Globals.SHOT_GLASS:
                sRend.sprite = lookAtShotGlassSprite;
                break;
            case Globals.FRIDGE:
                sRend.sprite = lookAtFridge;
                break;
        }
    }

}
