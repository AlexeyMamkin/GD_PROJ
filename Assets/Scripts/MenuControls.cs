using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void LevelChoised() //normal level
    {
        Utils.EventManager.Trigger("ClearField");
        Utils.EventManager.Trigger("EnableInGameUI");
        Utils.EventManager.Trigger("NormalMode");
        Utils.EventManager.Trigger("DisableMenu");
        
    }

    public void HardLevelChoised()
    {
        Utils.EventManager.Trigger("ClearField");
        Utils.EventManager.Trigger("EnableInGameUI");
        Utils.EventManager.Trigger("HardMode");
        Utils.EventManager.Trigger("DisableMenu");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("GameScene");
        Utils.EventManager.Trigger("ClearField");
        Utils.EventManager.Trigger("DisableInGameUI");
        Utils.EventManager.Trigger("EnableMenu");
    }
}
