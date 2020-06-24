using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float startTime = 30;
    public Text timer;
    private bool isOn = false;

    private void OnEnable()
    {
        Utils.EventManager.StartListening("StartGame", StartTimer);
        Utils.EventManager.StartListening("Win", StopTimer);
    }

    private void OnDisable()
    {
        Utils.EventManager.StopListening("StartGame", StartTimer);
        Utils.EventManager.StopListening("Win", StopTimer);
    }

    void Start()
    {
        timer.text = startTime.ToString();
    }
    
    void Update()
    {
        if (isOn)
        {
            if (startTime > 0)
            {
                startTime -= Time.deltaTime;
                timer.text = Mathf.Round(startTime).ToString();
            }
            else
            {
                Utils.EventManager.Trigger("Lose");
            }
        }
    }

    private void StartTimer()
    {
        isOn = true;
        startTime = 30;
    }

    private void StopTimer()
    {
        isOn = false;
    }
}
