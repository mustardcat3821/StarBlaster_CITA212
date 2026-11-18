using TMPro;
using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    int health = 100;

    void Start()
    {
        scoreText.text = "Health: " + health;
    }

    public void AddHealth(int healthPoints)
    {
        health += healthPoints;
        scoreText.text = "Health: " + health;
    }

    public void SubtractHealth(int damage)
    {
        health -= damage;
        scoreText.text = "Health: " + health;
        if (health <= 0)
        {
            health = 0;
            scoreText.text = "Health: " + health;
        }
    }
}
