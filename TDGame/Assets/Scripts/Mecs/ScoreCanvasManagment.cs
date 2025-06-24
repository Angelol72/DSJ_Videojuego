using UnityEngine;
using TMPro;

public class ScoreCanvasManagment : MonoBehaviour
{
    void Update()
    {
        TMP_Text scoreText = GetComponent<TMP_Text>();

        //Refactor
        if (scoreText == null)
        {
            return;
        }

        // find player by tag
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player == null)
        {
            return;
        }

        // Update the score text with the player's score
        scoreText.text = "Score: " + player.score.ToString();
    }
}
