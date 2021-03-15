using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {
    public static UIManager instance;
    public TextMeshProUGUI scoreText, hiscoreText;

    private int score = 0;
    private int hiscore = 0;

    private void Awake() {
        instance = this;
    }

    public void AddScore(int amount) {
        score += amount;
        SetScoreText(scoreText, score);
        if(score > hiscore) {
            hiscore = score;
            SetScoreText(hiscoreText, hiscore);
        }
    }

    public void ResetScore() {
        score = 0;
        SetScoreText(scoreText, score);
    }

    private void SetScoreText(TextMeshProUGUI text, int score) {
        text.text = score.ToString("D4");
    }
}
