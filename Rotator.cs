using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float value;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0, value);
    }
}
