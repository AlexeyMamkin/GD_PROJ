using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMode : MonoBehaviour
{
    private Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        Utils.EventManager<Mode>.StartListening("ShowMode", Show);
    }

    private void OnDisable()
    {
        Utils.EventManager<Mode>.StopListening("ShowMode", Show);
    }

    private void Show(Mode mode)
    {
        if (mode == Mode.Normal)
        {
            text.text = "";
            text.text = "Mode: Normal";
        }
        else
        {
            text.text = "";
            text.text = "Mode: Hard";
        }
    }
}
