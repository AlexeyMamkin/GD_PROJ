using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLevel : MonoBehaviour
{
    private Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        Utils.EventManager<int>.StartListening("ShowLevel", Show);
    }

    private void OnDisable()
    {
        Utils.EventManager<int>.StopListening("ShowLevel", Show);
    }

    private void Show(int level)
    {
        text.text = level.ToString() + " level";
    }
}
