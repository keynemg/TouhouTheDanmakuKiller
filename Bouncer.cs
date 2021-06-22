using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.parent.GetComponent<Knife>().Collide(collision);
    }
}
