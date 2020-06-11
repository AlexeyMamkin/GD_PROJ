using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public void SetPositionFor(int leftCount, int rightCount)
    {
        transform.Translate(new Vector3(-4.5f + 9f/(leftCount + rightCount) * leftCount, 0, 0));
    }
}
