using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HighScoreMenu : MonoBehaviour
{
    private Label result;
    private Label score1;
    private Label score2;
    private Label score3;
    private Label score4;
    private Label score5;

    private Button playAgain;
    private VisualElement root;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        result = root.Q<Label>("label-result");
        score1 = root.Q<Label>("score1");
        score2 = root.Q<Label>("score2");
        score3 = root.Q<Label>("score3");
        score4 = root.Q<Label>("score4");
        score5 = root.Q<Label>("score5");

        playAgain = root.Q<Button>("play-again");

        playAgain.clicked += PlayAgain_clicked;

        if (HighScoreManager.Get.GameIsWon)
        {
            result.text = "You Won!";
        } else
        {
            result.text = "You Lost";
        }

        var scores = HighScoreManager.Get.HighScores;

        if (scores.Count > 0) score1.text = scores[0].Name + ": " + scores[0].Score;
        if (scores.Count > 1) score2.text = scores[1].Name + ": " + scores[1].Score;
        if (scores.Count > 2) score3.text = scores[2].Name + ": " + scores[2].Score;
        if (scores.Count > 3) score4.text = scores[3].Name + ": " + scores[3].Score;
        if (scores.Count > 4) score5.text = scores[4].Name + ": " + scores[4].Score;
    }

    private void PlayAgain_clicked()
    {
        SceneManager.LoadScene("GameScene");
    }
}
