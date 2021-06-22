using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSlider : MonoBehaviour
{
    public float scroll_Speed;

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward*scroll_Speed, Space.World);
    }
}
