using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    private Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void Init()
    {
        transform.position = startPos;
    }

    public void SetPositionFor(int leftCount, int rightCount)
    {
        Init();
        transform.Translate(new Vector3(-4.5f + 9f/(leftCount + rightCount) * leftCount, 0, 0));
    }
}
