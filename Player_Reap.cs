using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Reap : MonoBehaviour
{

    public GameObject scythe_Prefab;
    public Transform scythe_Parent;
    public List<GameObject> scythes_List = new List<GameObject>();

    void Update()
    {
        if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.X))
        {
            if (Vector2.Distance(Player_Throw.instance.knife_Path[0], Player_Throw.instance.knife_Path[1]) <= 3)
            {
                ChooseScythe();
                PlayerStats.instance.shot_Count = 0;
                GameManager.Instance.combo_Counter = 0;
                GameManager.Instance.UpdateCombo();
                PlayerStats.instance.action_List.Add(ACTIONTYPE.Scythe);
                if (PlayerStats.instance.action_List.Count > 3)
                {
                    PlayerStats.instance.action_List.RemoveAt(0);
                }
                GameManager.Instance.UpdateActions(PlayerStats.instance.action_List);
            }
        }
    }

    #region Scythe
    public void ChooseScythe()
    {
        for (int i = 0; i < scythes_List.Count; i++)
        {
            if (!scythes_List[i].activeInHierarchy)
            {
                StartCoroutine(ReapFunc(scythes_List[i]));
                return;
            }
        }
        GameObject newobj = Instantiate(scythe_Prefab, scythe_Parent);
        scythes_List.Add(newobj);
        StartCoroutine(ReapFunc(newobj));
    }

    IEnumerator ReapFunc(GameObject _target)
    {
        _target.SetActive(true);
        _target.GetComponent<ScytheRotator>().enabled = true;

        if (PlayerStats.instance.shot_Count <= 3)
        {
            _target.GetComponent<ScytheRotator>().value = 360;
            _target.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            if (PlayerStats.instance.shot_Count <= 6)
            {
                _target.GetComponent<ScytheRotator>().value = 540;
                _target.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                _target.GetComponent<ScytheRotator>().value = 720;
                _target.transform.localScale = new Vector3(2f, 2f, 2f);
            }
        }
        float timerMultiplier;
        if (PlayerStats.instance.action_List[PlayerStats.instance.action_List.Count - 1] == ACTIONTYPE.Scythe)
        {
            if (PlayerStats.instance.action_List[PlayerStats.instance.action_List.Count - 2] == ACTIONTYPE.Scythe)
            {
                timerMultiplier = 3f;
            }
            else
            {
                timerMultiplier = 2f;
            }
        }
        else
        {
            timerMultiplier = 1f;
        }

        GameObject spriteCol = _target.transform.GetChild(0).gameObject;
        _target.transform.position = GameManager.Instance.mousePos;
        spriteCol.GetComponent<SpriteRenderer>().enabled = true;
        spriteCol.GetComponent<BoxCollider2D>().enabled = true;
        spriteCol.GetComponent<CapsuleCollider2D>().enabled = true;
        spriteCol.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        spriteCol.transform.GetChild(1).GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(timerMultiplier);
        spriteCol.transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
        spriteCol.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        _target.GetComponent<ScytheRotator>().enabled = false;
        spriteCol.GetComponent<SpriteRenderer>().enabled = false;
        spriteCol.GetComponent<BoxCollider2D>().enabled = false;
        spriteCol.GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        _target.SetActive(false);
    }
    #endregion

}
