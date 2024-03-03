using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class IntroMenu : MonoBehaviour
{
    private TextField name;
    private Button startButton;
    private Button quitButton;

    private VisualElement root;
    
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        
        name = root.Q<TextField>("textfield-name");
        startButton = root.Q<Button>("button-start-game");
        quitButton = root.Q<Button>("button-quit");

        if (name != null)
        {
            name.RegisterValueChangedCallback(evt =>
            {
                OnNameValueChanged(evt.newValue);
            });
        }

        if (startButton != null)
        {
            startButton.clicked += StartButton_clicked;
            startButton.SetEnabled(false);
        }

        if (quitButton != null)
        {
            quitButton.clicked += QuitButton_clicked;
        }
    }

    private void OnNameValueChanged(string value)
    {
        HighScoreManager.Get.PlayerName = value;
        startButton.SetEnabled(value.Length > 2);
    }

    private void QuitButton_clicked()
    {
        SoundManager.Get.PlayUISound();
        Application.Quit();
    }

    private void StartButton_clicked()
    {
        SoundManager.Get.PlayUISound();
        SceneManager.LoadScene("GameScene");
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        if (startButton != null)
        {
            startButton.clicked -= StartButton_clicked;
        }

        if (quitButton != null)
        {
            quitButton.clicked -= QuitButton_clicked;
        }
    }
}
