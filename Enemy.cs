using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool alive;
    public float speed;
    public string player_Tag;

    private void OnEnable()
    {
        alive = true;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        StopAllCoroutines();
        StartCoroutine(SelfDeactivate());
    }
    private void Start()
    {
        alive = true;
        StartCoroutine(SelfDeactivate());
    }

    private void FixedUpdate()
    {
        if (alive)
        {
            transform.Translate(Vector3.forward * speed);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(player_Tag))
        {
            alive = false;
            GameManager.Instance.score_Counter++;
            GameManager.Instance.UpdateScore();
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Stop();
            transform.GetChild(1).GetComponent<ParticleSystem>().Play();
            transform.GetChild(1).GetChild(0).GetComponent<ParticleSystem>().Play();

            StartCoroutine(Deactivate());
        }
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(1f);
        transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
        transform.GetChild(1).GetChild(0).GetComponent<ParticleSystem>().Stop();
        gameObject.SetActive(false);
    }


    IEnumerator SelfDeactivate()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }
}
