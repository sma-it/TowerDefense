using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TopMenu : MonoBehaviour
{
    private Label waveLabel;
    private Label healthLabel;
    private Label creditsLabel;
    private Button waveButton;

    private VisualElement root;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        waveLabel = root.Q<Label>("label-wave");
        healthLabel = root.Q<Label>("label-health");
        creditsLabel = root.Q<Label>("label-credits");
        waveButton = root.Q<Button>("button-start-wave");

        if (waveButton != null)
        {
            waveButton.clicked += WaveButton_clicked;
        }
    }

    public void EnableWaveButton()
    {
        waveButton.SetEnabled(true);
    }

    private void WaveButton_clicked()
    {
        GameManager.Get.StartWave();
        waveButton.SetEnabled(false);
    }

    public void SetWaveLabel(string text)
    {
        waveLabel.text = text;
    }

    public void SetCreditsLabel(string text)
    {
        creditsLabel.text = text;
    }

    public void SetHealthLabel(string text)
    {
        healthLabel.text = text;
    }


    private void OnDestroy()
    {
        if (waveButton != null)
        {
            waveButton.clicked -= WaveButton_clicked;
        }
    }
}
