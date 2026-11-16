using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    int score = 0;
    int winningScore = 100;

    void Start()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
        if (score >= winningScore)
        {
            scoreText.text = "Score: " + score + " - You win!";
            Debug.Log("You win!");
        }
    }

    public void SubtractScore(int points)
    {
        score -= points;
        scoreText.text = "You lost " + points + " points!";
        StartCoroutine(WaitForSeconds());

        if (score < 0)
        {
            score = 0;
            scoreText.text = "Score: " + score;
        }
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(2f);
    }
}
