using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerMenu : MonoBehaviour
{
    private Button archerButton;
    private Button swordButton;
    private Button wizardButton;
    private Button updateButton;
    private Button destroyButton;

    private ConstructionSite selectedSite;
    private VisualElement root;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        archerButton = root.Q<Button>("archer-button");
        swordButton = root.Q<Button>("sword-button");
        wizardButton = root.Q<Button>("wizard-button");
        updateButton = root.Q<Button>("button-upgrade");
        destroyButton = root.Q<Button>("button-destroy");

        if (archerButton != null)
        {
            archerButton.clicked += OnArcherButtonClicked;
        }

        if (swordButton != null)
        {
            swordButton.clicked += OnSwordButtonClicked;
        }

        if (wizardButton != null)
        {
            wizardButton.clicked += OnWizardButtonClicked;
        }

        if (updateButton != null)
        {
            updateButton.clicked += OnUpdateButtonClicked;
        }

        if (destroyButton != null)
        {
            destroyButton.clicked += OnDestroyButtonClicked;
        }

        root.visible = false;
    }

    private void OnArcherButtonClicked()
    {
        SoundManager.Get.PlayUISound();
        GameManager.Get.Build(TowerType.Archer, SiteLevel.Level1);
        EvaluateMenu();
    }

    private void OnSwordButtonClicked()
    {
        SoundManager.Get.PlayUISound();
        GameManager.Get.Build(TowerType.Sword, SiteLevel.Level1);
        EvaluateMenu();
    }

    private void OnWizardButtonClicked()
    {
        SoundManager.Get.PlayUISound();
        GameManager.Get.Build(TowerType.Wizard, SiteLevel.Level1);
        EvaluateMenu();
    }

    private void OnUpdateButtonClicked()
    {
        SoundManager.Get.PlayUISound();
        SiteLevel  level = selectedSite.Level;
        level++;
        GameManager.Get.Build(selectedSite.TowerType, level);
        EvaluateMenu();
    }

    private void OnDestroyButtonClicked()
    {
        SoundManager.Get.PlayUISound();
        GameManager.Get.Build(TowerType.Archer, SiteLevel.Level0);
        
        EvaluateMenu();
    }

    public void SetSite(ConstructionSite site)
    {
        selectedSite = site;
        if (selectedSite == null)
        {
            root.visible = false;
            return;
        }


        root.visible = true;
        EvaluateMenu();

    }

    public void EvaluateMenu()
    {
        if (selectedSite == null) return;

        int credits = GameManager.Get.GetCredits();

        switch (selectedSite.Level)
        {
            case SiteLevel.Level0:
                archerButton.SetEnabled(credits >= GameManager.Get.GetCost(TowerType.Archer, SiteLevel.Level1));
                swordButton.SetEnabled(credits >= GameManager.Get.GetCost(TowerType.Sword, SiteLevel.Level1));
                wizardButton.SetEnabled(credits >= GameManager.Get.GetCost(TowerType.Wizard, SiteLevel.Level1));
                updateButton.SetEnabled(false);
                destroyButton.SetEnabled(false);
                break;
            case SiteLevel.Level1:
            case SiteLevel.Level2:
                archerButton.SetEnabled(false);
                swordButton.SetEnabled(false);
                wizardButton.SetEnabled(false);
                updateButton.SetEnabled(credits >= GameManager.Get.GetCost(selectedSite.TowerType, selectedSite.Level + 1));
                destroyButton.SetEnabled(true);
                break;
            case SiteLevel.Level3:
                archerButton.SetEnabled(false);
                swordButton.SetEnabled(false);
                wizardButton.SetEnabled(false);
                updateButton.SetEnabled(false);
                destroyButton.SetEnabled(true);
                break;
        }
    }

    private void OnDestroy()
    {
        if (archerButton != null)
        {
            archerButton.clicked -= OnArcherButtonClicked;
        }

        if (swordButton != null)
        {
            swordButton.clicked -= OnSwordButtonClicked;
        }

        if (wizardButton != null)
        {
            wizardButton.clicked -= OnWizardButtonClicked;
        }

        if (updateButton != null)
        {
            updateButton.clicked -= OnUpdateButtonClicked;
        }

        if (destroyButton != null)
        {
            destroyButton.clicked -= OnArcherButtonClicked;
        }
    }
}
