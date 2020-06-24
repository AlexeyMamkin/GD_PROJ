using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject game;

    private void OnEnable()
    {
        Utils.EventManager.StartListening("EnableMenu", EnableMenu);
        Utils.EventManager.StartListening("DisableMenu", DisableMenu);
        Utils.EventManager.StartListening("EnableInGameUI", EnableUI);
        Utils.EventManager.StartListening("DisableInGameUI", DisableUI);
        Utils.EventManager.StartListening("EnableGame", EnableGame);
        Utils.EventManager.StartListening("DisableGame", DisableGame);
    }

    private void OnDisable()
    {
        Utils.EventManager.StopListening("EnableMenu", EnableMenu);
        Utils.EventManager.StopListening("DisableMenu", DisableMenu);
        Utils.EventManager.StopListening("EnableInGameUI", EnableUI);
        Utils.EventManager.StopListening("DisableInGameUI", DisableUI);
        Utils.EventManager.StopListening("EnableGame", EnableGame);
        Utils.EventManager.StopListening("DisableGame", DisableGame);
    }

    private void EnableMenu()
    {
        menu.SetActive(true);
    }

    private void DisableMenu()
    {
        menu.SetActive(false);
    }

    private void EnableUI()
    {
        inGameUI.SetActive(true);
    }

    private void DisableUI()
    {
        inGameUI.SetActive(false);
    }

    private void EnableGame()
    {
        game.SetActive(true);
    }

    private void DisableGame()
    {
        game.SetActive(false);
    }
}
