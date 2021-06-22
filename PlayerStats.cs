using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ACTIONTYPE {none,Shoot,Slash,Scythe,Knife}
public class PlayerStats : MonoBehaviour
{
    private static PlayerStats Instance;
    public static PlayerStats instance { get { return instance; } }

    public GameObject shot_Prefab;
    public Transform shot_Parent;
    public int shot_Count;
    public List<ParticleSystem> shots_List = new List<ParticleSystem>();

    public ParticleSystem slash_FX;
    public GameObject Slash_Prefab;
    public GameObject slash_Current;
    public Transform slash_Parent;
    public List<Vector3> slash_Path = new List<Vector3>();
    public List<Vector3> slash_Path_Echo_1 = new List<Vector3>();
    public List<Vector3> slash_Path_Echo_2 = new List<Vector3>();
    public List<GameObject> slash_List = new List<GameObject>();
    int slash_Echo;

    public GameObject scythe_Prefab;
    public Transform scythe_Parent;
    public List<GameObject> scythes_List = new List<GameObject>();

    public GameObject knife_Prefab;
    public Transform knife_Parent;
    public Vector3[] knife_Path;
    Transform center_Knife;
    public List<GameObject> knife_List = new List<GameObject>();

    public List<ACTIONTYPE> action_List = new List<ACTIONTYPE>(4);

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        slash_Path.Insert(0,Vector3.zero);
        knife_Path = new Vector3[2];
        action_List.Add(ACTIONTYPE.Shoot);
        action_List.Add(ACTIONTYPE.Shoot);
        action_List.Add(ACTIONTYPE.Shoot);
        action_List.Add(ACTIONTYPE.Shoot);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z))
        {
            slash_FX.transform.position = GameManager.Instance.mousePos;
            slash_FX.Play();
            slash_Path.Insert(0, GameManager.Instance.mousePos);
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
                if (action_List[2] == ACTIONTYPE.Slash)
                {
                    if (action_List[0] == ACTIONTYPE.Slash)
                    {
                        Slash(slash_Path_Echo_1);          
                        Slash(slash_Path_Echo_2);
                    }
                    else
                    {
                        Slash(slash_Path_Echo_1);          
                    }
                }


                shot_Count = 0;
                GameManager.Instance.combo_Counter = shot_Count;
                GameManager.Instance.UpdateCombo();

                action_List.Add(ACTIONTYPE.Slash);
                if (action_List.Count > 3)
                {
                    action_List.RemoveAt(0);
                }
                GameManager.Instance.UpdateActions(action_List);

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

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.X))
        {
            knife_Path[0] = GameManager.Instance.mousePos;
        }

        if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.X))
        {
            knife_Path[1] = GameManager.Instance.mousePos;
            if (Vector2.Distance(knife_Path[0], knife_Path[1]) > 3)
            {
                if (action_List[action_List.Count - 1] == ACTIONTYPE.Knife)
                {
                    if (action_List[action_List.Count - 2] == ACTIONTYPE.Knife)
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

                shot_Count = 0;
                GameManager.Instance.combo_Counter = 0;
                GameManager.Instance.UpdateCombo();
                action_List.Add(ACTIONTYPE.Knife);
                if (action_List.Count > 3)
                {
                    action_List.RemoveAt(0);
                }
                GameManager.Instance.UpdateActions(action_List);
            }
            else
            {
                ChooseScythe();
                shot_Count = 0;
                GameManager.Instance.combo_Counter = 0;
                GameManager.Instance.UpdateCombo();
                action_List.Add(ACTIONTYPE.Scythe);
                if (action_List.Count > 3)
                {
                    action_List.RemoveAt(0);
                }
                GameManager.Instance.UpdateActions(action_List);
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

    #region Slash
    void Slash(List<Vector3> _path)
    {
        ChooseSlash();

        float baseTimer;
        if (shot_Count <= 3)
        {
            slash_Current.transform.localScale = new Vector3(1f, 1f, 1f);
            baseTimer = 0.03f;
        }
        else
        {
            if (shot_Count <= 6)
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

        if (shot_Count <= 3)
        {
             _target.GetComponent<ScytheRotator>().value = 360;
             _target.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        }
        else
        {
            if (shot_Count <= 6)
            {
                _target.GetComponent<ScytheRotator>().value = 540;
                _target.transform.localScale = new Vector3(1f,1f,1f);
            }
            else
            {
                _target.GetComponent<ScytheRotator>().value = 720;
                _target.transform.localScale = new Vector3(2f,2f,2f);
            }
        }
        float timerMultiplier;
        if (action_List[action_List.Count-1] == ACTIONTYPE.Scythe)
        {
            if (action_List[action_List.Count-2] == ACTIONTYPE.Scythe)
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
            StartKnife(currentKnife,i);
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
        if (shot_Count >= 3)
        {
            if (shot_Count >= 6)
            {
                _target.transform.localScale = new Vector3(2f, 2f, 2f);
                _target.GetComponent<Knife>().bounce_Count = 10 + shot_Count * 4;
                _target.GetComponent<Knife>().speed = 2f;
            }
            else
            {
                _target.transform.localScale = new Vector3(1f, 1f, 1f);
                _target.GetComponent<Knife>().bounce_Count = 5 + shot_Count * 3;
                _target.GetComponent<Knife>().speed = 1f;
            }
        }
        else
        {
            _target.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            _target.GetComponent<Knife>().bounce_Count = 3 + shot_Count * 2;
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
