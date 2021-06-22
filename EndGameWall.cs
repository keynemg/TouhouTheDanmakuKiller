using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameWall : MonoBehaviour
{
    public GameObject EndGameScreen;
    public Text finalScore_Text;
    private void OnTriggerEnter(Collider other)
    {
        EndGameScreen.SetActive(true);
        GameManager.Instance.UpdateScore(finalScore_Text);
    }
}
