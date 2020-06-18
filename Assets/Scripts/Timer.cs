using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float startTime = 15;
    public Text timer;
    public GameObject losePanel;
    private bool isWin = false;

    void Start()
    {
        timer.text = startTime.ToString();
    }

    
    void Update()
    {
        if (startTime > 0 && !isWin)
        {
            startTime -= Time.deltaTime;
            timer.text = Mathf.Round(startTime).ToString();
        }
        else if (!isWin)
        {
            losePanel.SetActive(true);
        }
    }

    public void SetWin()
    {
        isWin = true;
    }
}
