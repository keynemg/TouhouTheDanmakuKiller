using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheRotator : MonoBehaviour
{
    public int value;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0, -value*Time.deltaTime);
    }
}
