using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void LevelChoised() //normal level
    {
        Utils.EventManager.Trigger("ClearField");

        Utils.EventManager.Trigger("NormalMode");
        Utils.EventManager.Trigger("DisableMenu");
    }

    public void HardLevelChoised()
    {
        Utils.EventManager.Trigger("ClearField");

        Utils.EventManager.Trigger("HardMode");
        Utils.EventManager.Trigger("DisableMenu");
    }

    public void ToMainMenu()
    {
        Utils.EventManager.Trigger("EnableMenu");
    }
}
