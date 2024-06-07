using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveHUDObject : HUDObject
{

    public GameSaveAndLoad gameSaver;

    private void Start()
    {
        BaseStart();
    }

    protected override void OnMouseDown()
    {
        gameSaver.SaveGame();
    }

}
