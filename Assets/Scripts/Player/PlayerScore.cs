using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "SCORE: " + score.ToString();
    }
}
