using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TMP_Text tmpText;
    int score;

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        tmpText.text = score.ToString();
    }
}
