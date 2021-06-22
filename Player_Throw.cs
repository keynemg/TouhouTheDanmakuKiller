using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Throw : MonoBehaviour
{
    private static Player_Throw Instance;
    public static Player_Throw instance { get { return instance; } }

    public GameObject knife_Prefab;
    public Transform knife_Parent;
    public Vector3[] knife_Path;
    Transform center_Knife;
    public List<GameObject> knife_List = new List<GameObject>();


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    { 
        knife_Path = new Vector3[2];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.X))
        {
            knife_Path[0] = GameManager.Instance.mousePos;
        }

        if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.X))
        {
            knife_Path[1] = GameManager.Instance.mousePos;
            if (Vector2.Distance(knife_Path[0], knife_Path[1]) > 3)
            {
                if (PlayerStats.instance.action_List[PlayerStats.instance.action_List.Count - 1] == ACTIONTYPE.Knife)
                {
                    if (PlayerStats.instance.action_List[PlayerStats.instance.action_List.Count - 2] == ACTIONTYPE.Knife)
                    {
                        ChooseKnife(5);
                    }
                    else
                    {
                        ChooseKnife(3);
                    }
                }
                else
                {
                    ChooseKnife();
                }

                PlayerStats.instance.shot_Count = 0;
                GameManager.Instance.combo_Counter = 0;
                GameManager.Instance.UpdateCombo();
                PlayerStats.instance.action_List.Add(ACTIONTYPE.Knife);
                if (PlayerStats.instance.action_List.Count > 3)
                {
                    PlayerStats.instance.action_List.RemoveAt(0);
                }
                GameManager.Instance.UpdateActions(PlayerStats.instance.action_List);
            }
            else
            {
                PlayerStats.instance.shot_Count = 0;
                GameManager.Instance.combo_Counter = 0;
                GameManager.Instance.UpdateCombo();
            }
        }
    }

    #region Knife

    void ChooseKnife()
    {
        for (int i = 0; i < knife_List.Count; i++)
        {
            if (!knife_List[i].activeInHierarchy)
            {
                StartKnife(knife_List[i]);
                return;
            }
        }
        GameObject newobj = Pooling.PoolThis(knife_Prefab);
        newobj.transform.SetParent(knife_Parent);
        knife_List.Add(newobj);
        StartKnife(newobj);
    }

    void ChooseKnife(int _numberOfKnives)
    {
        GameObject currentKnife;
        for (int i = 0; i < _numberOfKnives; i++)
        {
            for (int j = 0; j < knife_List.Count; j++)
            {
                if (!knife_List[j].activeInHierarchy)
                {
                    currentKnife = knife_List[j];
                }
            }
            GameObject newobj = Pooling.PoolThis(knife_Prefab);
            newobj.transform.SetParent(knife_Parent);
            knife_List.Add(newobj);
            currentKnife = newobj;
            StartKnife(currentKnife, i);
            if (_numberOfKnives == 3)
            {
                currentKnife.GetComponent<Knife>().speed = 0.4f;
                currentKnife.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                currentKnife.GetComponent<Knife>().bounce_Count = 3;
            }
            else
            {
                currentKnife.GetComponent<Knife>().speed = 0.2f;
                currentKnife.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                currentKnife.GetComponent<Knife>().bounce_Count = 2;
            }
        }
    }

    void StartKnife(GameObject _target)
    {
        _target.SetActive(true);
        if (PlayerStats.instance.shot_Count >= 3)
        {
            if (PlayerStats.instance.shot_Count >= 6)
            {
                _target.transform.localScale = new Vector3(2f, 2f, 2f);
                _target.GetComponent<Knife>().bounce_Count = 10 + PlayerStats.instance.shot_Count * 4;
                _target.GetComponent<Knife>().speed = 2f;
            }
            else
            {
                _target.transform.localScale = new Vector3(1f, 1f, 1f);
                _target.GetComponent<Knife>().bounce_Count = 5 + PlayerStats.instance.shot_Count * 3;
                _target.GetComponent<Knife>().speed = 1f;
            }
        }
        else
        {
            _target.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            _target.GetComponent<Knife>().bounce_Count = 3 + PlayerStats.instance.shot_Count * 2;
            _target.GetComponent<Knife>().speed = 0.5f;
        }
        _target.transform.position = knife_Path[1];
        _target.transform.LookAt(knife_Path[0], GameManager.Instance.transform.up);
        _target.transform.Rotate(0, 90, 180);
    }

    void StartKnife(GameObject _target, int _i)
    {
        _target.SetActive(true);
        if (_i == 0)
        {
            center_Knife = _target.transform;
            _target.transform.position = knife_Path[1];
            _target.transform.LookAt(knife_Path[0], GameManager.Instance.transform.up);
            _target.transform.Rotate(0, 90, 180);
        }
        else
        {
            if (_i % 2 == 0 && _i > 0)
            {
                if (_i == 2)
                {
                    _target.transform.SetParent(center_Knife);
                    _target.transform.localPosition = new Vector3(-1, -3, 0);
                    _target.transform.rotation = center_Knife.rotation;
                    _target.transform.Rotate(0, 0, -24);
                    _target.transform.SetParent(center_Knife.parent);
                }
                else
                {
                    _target.transform.SetParent(center_Knife);
                    _target.transform.localPosition = new Vector3(-3, -5.35f, 0);
                    _target.transform.rotation = center_Knife.rotation;
                    _target.transform.Rotate(0, 0, -50);
                    _target.transform.SetParent(center_Knife.parent);
                }
            }
            else
            {
                if (_i == 1)
                {
                    _target.transform.SetParent(center_Knife);
                    _target.transform.localPosition = new Vector3(-1, 3, 0);
                    _target.transform.rotation = center_Knife.rotation;
                    _target.transform.Rotate(0, 0, 25);
                    _target.transform.SetParent(center_Knife.parent);
                }
                else
                {
                    _target.transform.SetParent(center_Knife);
                    _target.transform.localPosition = new Vector3(-3, 5.35f, 0);
                    _target.transform.rotation = center_Knife.rotation;
                    _target.transform.Rotate(0, 0, 50);
                    _target.transform.SetParent(center_Knife.parent);
                }
            }
        }
    }
    #endregion
}
