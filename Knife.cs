using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float speed;
    GameObject sprite_And_Col;
    public int bounce_Count;


    private void Awake()
    {
        sprite_And_Col = transform.GetChild(0).gameObject;
        sprite_And_Col.GetComponent<SpriteRenderer>().enabled = true;
        sprite_And_Col.GetComponent<CircleCollider2D>().enabled = true;
        sprite_And_Col.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        sprite_And_Col.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
    }
    private void OnEnable()
    {
        sprite_And_Col.GetComponent<SpriteRenderer>().enabled = true;
        sprite_And_Col.GetComponent<CircleCollider2D>().enabled = true;
        sprite_And_Col.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        sprite_And_Col.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed);
    }

    public void Collide(Collision2D _target)
    {
        if (bounce_Count > 0)
        {
            bounce_Count--;
            transform.GetChild(0).GetChild(2).GetComponent<ParticleSystem>().Play();
            transform.rotation = Quaternion.FromToRotation(Vector2.right, Vector2.Reflect(transform.TransformDirection(Vector2.right), _target.GetContact(0).normal));
        }
        else
        {
            Disable();
        }
    }

    public void Disable()
    {
        sprite_And_Col.transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
        sprite_And_Col.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        sprite_And_Col.GetComponent<SpriteRenderer>().enabled = false;
        sprite_And_Col.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.SetActive(false);
        bounce_Count = 0;
    }
}
