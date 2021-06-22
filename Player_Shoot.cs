using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot : MonoBehaviour
{

    public GameObject shot_Prefab;
    public Transform shot_Parent;
    public int shot_Count;
    public List<ParticleSystem> shots_List = new List<ParticleSystem>();

    public List<ACTIONTYPE> action_List = new List<ACTIONTYPE>(4);

    private void Start()
    {
        action_List.Add(ACTIONTYPE.Shoot);
        action_List.Add(ACTIONTYPE.Shoot);
        action_List.Add(ACTIONTYPE.Shoot);
        action_List.Add(ACTIONTYPE.Shoot);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z))
        {
            ShootFunc();
            shot_Count++;
            GameManager.Instance.combo_Counter++;
            GameManager.Instance.UpdateCombo();
            action_List.Add(ACTIONTYPE.Shoot);
            if (action_List.Count > 3)
            {
                action_List.RemoveAt(0);
            }
            GameManager.Instance.UpdateActions(action_List);
        }

    }

    #region Shoot
    public void ShootFunc()
    {
        for (int i = 0; i < shots_List.Count; i++)
        {
            if (!shots_List[i].isPlaying)
            {
                StartShot(shots_List[i]);
                shots_List[i].transform.position = GameManager.Instance.mousePos;
                shots_List[i].Play();
                return;
            }
        }
        GameObject newobj = Pooling.PoolThis(shot_Prefab);
        newobj.transform.SetParent(shot_Parent);
        shots_List.Add(newobj.GetComponent<ParticleSystem>());
        StartShot(newobj.GetComponent<ParticleSystem>());
    }

    void StartShot(ParticleSystem _target)
    {
        StartCoroutine(ShotDamageActivation(_target));
        _target.transform.position = GameManager.Instance.mousePos;
        _target.Play();
    }

    IEnumerator ShotDamageActivation(ParticleSystem __target)
    {
        __target.GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.02f);
        __target.GetComponent<CircleCollider2D>().enabled = false;
    }
    #endregion
}
