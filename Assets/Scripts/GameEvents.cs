using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LosePanel;

    private void OnEnable()
    {
        Utils.EventManager.StartListening("ClearField", DeactivatePanel);
        Utils.EventManager.StartListening("Win", Win);
        Utils.EventManager.StartListening("Lose", Lose);
    }

    private void OnDisable()
    {
        Utils.EventManager.StopListening("ClearField", DeactivatePanel);
        Utils.EventManager.StopListening("Win", Win);
        Utils.EventManager.StopListening("Lose", Lose);
    }

    public void Win()
    {
        WinPanel.SetActive(true);
    }
    public void Lose()
    {
        LosePanel.SetActive(true);
    }

    public void DeactivatePanel()
    {
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
    }

    public void ToNextLevel()
    {
        Utils.EventManager.Trigger("LoadNextLevel");
        DeactivatePanel();
    }

    public void RestartLevel()
    {
        Utils.EventManager.Trigger("LoadCurrentLevel");
        DeactivatePanel();
    }
}
