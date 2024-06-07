using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameHUDObject : HUDObject
{

    public GameSaveAndLoad gameSaver;

    private void Start()
    {
        BaseStart();
    }

    protected override void OnMouseDown()
    {
        SceneManager.LoadScene("Bedroom");
    }

}
