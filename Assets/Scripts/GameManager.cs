using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // singleton code
    private static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static GameManager Get { get => instance; }

    public GameObject TowerMenu;
    public GameObject TopMenu;

    public List<GameObject> Archers = new List<GameObject>();
    public List<GameObject> Swords = new List<GameObject>();
    public List<GameObject> Wizards = new List<GameObject>();

    private ConstructionSite SelectedSite;
    private TowerMenu towerMenu;
    private TopMenu topMenu;

    private int credits = 200;
    private int health = 10;
    private int currentWave = 0;
    private bool waveActive = false;
    public int ufoInGameCounter = 0;


    private void Start()
    {
        towerMenu = TowerMenu.GetComponent<TowerMenu>();
        topMenu = TopMenu.GetComponent<TopMenu>();
    }

    public void StartGame()
    {
        credits = 200;
        health = 10;
        currentWave = 0;
        waveActive = false;
        ufoInGameCounter = 0;
    }

    public void StartWave()
    {
        ufoInGameCounter = 0;
        currentWave++;
        UFOSpawner.Get.StartWave(currentWave);
        topMenu.SetWaveLabel("Wave: " + currentWave);
        waveActive = true;
        SoundManager.Get.PlayFXSound(0);
    }

    public void EndWave()
    {
        
        waveActive = false;
        
    }

    public void AddInGameUFO()
    {
        ufoInGameCounter++;
    }

    public void RemoveInGameUFO()
    {
        ufoInGameCounter--;
        if (!waveActive && ufoInGameCounter <= 0)
        {
            if (currentWave == 8)
            {
                // game is done
                HighScoreManager.Get.GameIsWon = true;
                HighScoreManager.Get.AddHighScore(credits);
                SoundManager.Get.PlayFXSound(5);
                SceneManager.LoadScene("HighScoreScene");
            }
            else
            {
                SoundManager.Get.PlayFXSound(4);
                topMenu.EnableWaveButton();
            }
            
        } 
    }

    public void AttackGate()
    {
        health--;
        topMenu.SetHealthLabel("Gate Health: " + health);

        if (health == 0)
        {
            // game is lost
            HighScoreManager.Get.GameIsWon = false;
            SoundManager.Get.PlayFXSound(3);
            SceneManager.LoadScene("HighScoreScene");
        } else
        {
            SoundManager.Get.PlayFXSound(2);
        }
    }

    public void AddCredits(int amount)
    {
        credits += amount;
        topMenu.SetCreditsLabel("Credits: " + credits);
        towerMenu.EvaluateMenu();
    }

    public void RemoveCredits(int amount)
    {
        credits -= amount;
        topMenu.SetCreditsLabel("Credits: " + credits);
    }

    public int GetCredits()
    {
        return credits;
    }

    public void OnSiteSelect(ConstructionSite site)
    {
        SelectedSite = site;
        if (towerMenu != null)
        {
            towerMenu.SetSite(SelectedSite);
        }
    }

    public void Build(TowerType type, SiteLevel level)
    {
        if (SelectedSite != null)
        {
            if (level == SiteLevel.Level0)
            {
                AddCredits(GetCost(SelectedSite.TowerType, SelectedSite.Level, true));
            } else
            {
                RemoveCredits(GetCost(type, level));
            }


            List<GameObject> list;
            GameObject tower = null;
            switch(type)
            {
                case TowerType.Archer: 
                    list = Archers;
                    break;
                case TowerType.Sword:
                    list = Swords;
                    break;
                case TowerType.Wizard:
                    list = Wizards;
                    break;
                default: list = Wizards;
                    break;
            }

            switch(level)
            {
                case SiteLevel.Level1:
                    tower = Instantiate(list[0], SelectedSite.WorldPosition, Quaternion.identity);
                    break;
                case SiteLevel.Level2:
                    tower = Instantiate(list[1], SelectedSite.WorldPosition, Quaternion.identity);
                    break;
                case SiteLevel.Level3:
                    tower = Instantiate(list[2], SelectedSite.WorldPosition, Quaternion.identity);
                    break;
            }

            if (tower != null) tower.GetComponent<Tower>().type = type;
            SelectedSite.SetTower(tower, level, type);
            towerMenu.SetSite(null);
        }
    }

    public int GetCost(TowerType type, SiteLevel level, bool selling = false)
    {
        int cost = 0;
        switch(type)
        {
            case TowerType.Archer:
                {
                    switch(level)
                    {
                        case SiteLevel.Level1:
                            cost = 100;
                            break;
                        case SiteLevel.Level2:
                            cost = 150;
                            break;
                        case SiteLevel.Level3:
                            cost = 200;
                            break;
                    }
                    break;
                }
            case TowerType.Sword:
                {
                    switch (level)
                    {
                        case SiteLevel.Level1:
                            cost = 150;
                            break;
                        case SiteLevel.Level2:
                            cost = 225;
                            break;
                        case SiteLevel.Level3:
                            cost = 300;
                            break;
                    }
                    break;
                }
            case TowerType.Wizard:
                {
                    switch (level)
                    {
                        case SiteLevel.Level1:
                            cost = 200;
                            break;
                        case SiteLevel.Level2:
                            cost = 300;
                            break;
                        case SiteLevel.Level3:
                            cost = 400;
                            break;
                    }
                    break;
                }
        }

        if (selling)
        {
            return cost / 2;
        } else
        {
            return cost;
        }
    }
}
