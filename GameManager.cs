using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static private GameManager instance;
    static public GameManager Instance { get { return instance; } } 

    public Text score_Text;
    public int score_Counter;
    public Text combo_Text;
    public Text actions_Text;
    public int combo_Counter;

    public Vector3 mousePos;

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        score_Text.text = score_Counter.ToString();
    }

    public void UpdateActions(List<ACTIONTYPE> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            actions_Text.transform.GetChild(i).GetComponent<Text>().text = _list[i].ToString();
        }
    }
    public void UpdateScore()
    {
        score_Text.text = score_Counter.ToString();
    }
    
    public void UpdateScore(Text _ui)
    {
        _ui.text = score_Counter.ToString();
    }
    public void UpdateCombo()
    {
        combo_Text.text = combo_Counter.ToString();
    }

}
