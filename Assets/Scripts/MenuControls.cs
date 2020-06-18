using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void LevelChoised() //normal level
    {
        SceneManager.LoadScene("GameScene");
    }

    public void HardLevelChoised()
    {
        Debug.Log("--------------Hard level in development--------------");
    }
    
    public void ToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
