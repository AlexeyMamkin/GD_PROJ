using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{
    [SerializeField] private GameObject WinPanel;

    public void Win()
    {
        WinPanel.SetActive(true);
    }

    public void ToNextLevel()
    {
        Debug.Log("--------------In development--------------");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
