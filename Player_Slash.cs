using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Slash : MonoBehaviour
{

    private static Player_Slash Instance;
    public static Player_Slash instance { get { return instance; } }


    public ParticleSystem slash_FX;
    public GameObject Slash_Prefab;
    public GameObject slash_Current;
    public Transform slash_Parent;
    public List<Vector3> slash_Path = new List<Vector3>();
    public List<Vector3> slash_Path_Echo_1 = new List<Vector3>();
    public List<Vector3> slash_Path_Echo_2 = new List<Vector3>();
    public List<GameObject> slash_List = new List<GameObject>();
    int slash_Echo;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        slash_Path.Insert(0, Vector3.zero);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z))
        {
            slash_FX.transform.position = GameManager.Instance.mousePos;
            slash_FX.Play();
            slash_Path.Insert(0, GameManager.Instance.mousePos);

            GameManager.Instance.combo_Counter++;
            GameManager.Instance.UpdateCombo();
        }

        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Z))
        {
            slash_FX.transform.position = GameManager.Instance.mousePos;
        }

        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Z))
        {
            slash_FX.Stop();
            if (slash_Path.Count > 4)
            {
                Slash(slash_Path);
                if (PlayerStats.instance.action_List[2] == ACTIONTYPE.Slash)
                {
                    if (PlayerStats.instance.action_List[0] == ACTIONTYPE.Slash)
                    {
                        Slash(slash_Path_Echo_1);
                        Slash(slash_Path_Echo_2);
                    }
                    else
                    {
                        Slash(slash_Path_Echo_1);
                    }
                }


                PlayerStats.instance.shot_Count = 0;
                GameManager.Instance.combo_Counter = PlayerStats.instance.shot_Count;
                GameManager.Instance.UpdateCombo();

                PlayerStats.instance.action_List.Add(ACTIONTYPE.Slash);
                if (PlayerStats.instance.action_List.Count > 3)
                {
                    PlayerStats.instance.action_List.RemoveAt(0);
                }
                GameManager.Instance.UpdateActions(PlayerStats.instance.action_List);

                slash_Path_Echo_2.Clear();
                for (int i = 0; i < slash_Path_Echo_1.Count; i++)
                {
                    slash_Path_Echo_2.Add(slash_Path_Echo_1[i]);
                }
                slash_Path_Echo_1.Clear();
                for (int i = 0; i < slash_Path.Count; i++)
                {
                    slash_Path_Echo_1.Add(slash_Path[i]);
                }
            }
            if (slash_Path.Count > 0)
            {
                slash_Path.Clear();
            }
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (slash_Path.Count >= 1)
            {
                if (Vector2.Distance(GameManager.Instance.mousePos, slash_Path[slash_Path.Count - 1]) > 1)
                {
                    slash_Path.Add(GameManager.Instance.mousePos);
                }
            }
        }
    }

    #region Slash
    void Slash(List<Vector3> _path)
    {
        ChooseSlash();

        float baseTimer;
        if (PlayerStats.instance.shot_Count <= 3)
        {
            slash_Current.transform.localScale = new Vector3(1f, 1f, 1f);
            baseTimer = 0.03f;
        }
        else
        {
            if (PlayerStats.instance.shot_Count <= 6)
            {
                slash_Current.transform.localScale = new Vector3(3f, 3f, 3f);
                baseTimer = 0.02f;
            }
            else
            {
                slash_Current.transform.localScale = new Vector3(5f, 5f, 5f);
                baseTimer = 0.01f;
            }
        }

        iTween.MoveTo(slash_Current.gameObject, iTween.Hash(
            "path", _path.ToArray(),
            "movetopath", false,
            "easetype", iTween.EaseType.linear,
            "time", baseTimer * _path.Count));


        StartCoroutine(SlashDamageActivation(slash_Current, _path.Count));
    }

    void ChooseSlash()
    {
        for (int i = 0; i < slash_List.Count; i++)
        {
            if (!slash_List[i].activeInHierarchy)
            {
                slash_Current = slash_List[i];
                slash_Current.SetActive(true);
                slash_Current.transform.position = slash_Path[0];
                slash_Current.GetComponent<ParticleSystem>().Play();
                return;
            }
        }

        GameObject newobj = Pooling.PoolThis(Slash_Prefab);
        newobj.transform.SetParent(slash_Parent);
        slash_List.Add(newobj);
        slash_Current = newobj;
    }

    IEnumerator SlashDamageActivation(GameObject _target, float _time)
    {
        _target.GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSecondsRealtime(0.1f * _time);
        _target.GetComponent<CircleCollider2D>().enabled = false;
        _target.GetComponent<ParticleSystem>().Stop();
        _target.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSecondsRealtime(1f);
        _target.SetActive(false);
        slash_Current = null;
    }
    #endregion

}
