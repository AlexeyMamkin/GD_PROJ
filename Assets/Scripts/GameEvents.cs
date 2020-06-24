using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{
    [SerializeField] private GameObject WinPanel;

    private void OnEnable()
    {
        Utils.EventManager.StartListening("ClearField", DeactivatePanel);
    }

    private void OnDisable()
    {
        Utils.EventManager.StopListening("ClearField", DeactivatePanel);
    }

    public void Win()
    {
        WinPanel.SetActive(true);
    }

    public void DeactivatePanel()
    {
        WinPanel.SetActive(false);
    }

    public void ToNextLevel()
    {
        Utils.EventManager.Trigger("LoadNextLevel");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        DeactivatePanel();
    }

    public void RestartLevel()
    {
        Utils.EventManager.Trigger("LoadCurrentLevel");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        DeactivatePanel();
    }
}
