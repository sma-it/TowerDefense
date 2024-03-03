using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    // singleton code
    private static UFOSpawner instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static UFOSpawner Get { get => instance; } 


    // game logic

    public List<GameObject> Path1 = new List<GameObject>();
    public List<GameObject> Path2 = new List<GameObject>();

    public List<GameObject> ufos = new List<GameObject>();
    private int ufoCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnUFO", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnUFO(int type, Path path)
    {
        var newUFO = Instantiate(ufos[type], Path1[0].transform.position, Path1[0].transform.rotation);
        var script = newUFO.GetComponentInParent<UFO>();

        script.path = path;
        script.target = Path1[1];
        script.ID = ufoCounter;
        GameManager.Get.AddInGameUFO();
    }

    public GameObject RequestTarget(Path path, int index)
    {
        // decide which path to use
        List<GameObject> list = path == Path.Path1 ? Path1 : Path2;
        
        // increment current index
        index++;

        // check if end of path is reached
        if (index == list.Count) return null;

        return list[index];
    } 

    public void StartWave(int number)
    {
        // reset counter
        ufoCounter = 0;

        switch(number)
        {
            case 1: InvokeRepeating("StartWave1", 1f, 1.5f);
                break;
            case 2:
                InvokeRepeating("StartWave2", 1f, 1.4f);
                break;
            case 3:
                InvokeRepeating("StartWave3", 1f, 1.3f);
                break;
            case 4:
                InvokeRepeating("StartWave4", 1f, 1.2f);
                break;
            case 5:
                InvokeRepeating("StartWave5", 1f, 1.1f);
                break;
            case 6:
                InvokeRepeating("StartWave6", 1f, 1f);
                break;
            case 7:
                InvokeRepeating("StartWave7", 1f, 0.9f);
                break;
            case 8:
                InvokeRepeating("StartWave8", 1f, 0.8f);
                break;
        }
    }

    public void StartWave1()
    {
        ufoCounter++;
        // leave some gaps
        if (ufoCounter % 6 <= 1) return;

        if (ufoCounter < 30)
        {
            SpawnUFO(0, Path.Path1);
        } else
        {
            SpawnUFO(1, Path.Path1);
        }

        if (ufoCounter > 30) {
            CancelInvoke("StartWave1");
            GameManager.Get.EndWave();
        }
    }

    public void StartWave2()
    {
        ufoCounter++;
        // leave some gaps
        if (ufoCounter % 8 <= 1) return;

        if (ufoCounter < 30)
        {
            SpawnUFO(Random.Range(0, 2), Path.Path1);
        }
        else
        {
            SpawnUFO(2, Path.Path1);
        }

        if (ufoCounter > 40)
        {
            CancelInvoke("StartWave2");
            GameManager.Get.EndWave();
        }
    }

    public void StartWave3()
    {
        ufoCounter++;
        // leave some gaps
        if (ufoCounter % 8 <= 1) return;

        if (ufoCounter < 30)
        {
            SpawnUFO(Random.Range(0, 3), Path.Path1);
        }
        else if (ufoCounter < 40)
        {
            SpawnUFO(Random.Range(1, 3), Path.Path1);
        } else
        {
            SpawnUFO(Random.Range(2, 4), Path.Path1);
        }

        if (ufoCounter > 50)
        {
            CancelInvoke("StartWave3");
            GameManager.Get.EndWave();
        }
    }

    public void StartWave4()
    {
        ufoCounter++;
        // leave some gaps
        if (ufoCounter % 10 <= 1) return;

        if (ufoCounter < 20)
        {
            SpawnUFO(Random.Range(0, 4), Path.Path1);
        }
        else if (ufoCounter < 40)
        {
            SpawnUFO(Random.Range(2, 4), Path.Path1);
        }
        else
        {
            SpawnUFO(Random.Range(2, 5), Path.Path1);
        }

        if (ufoCounter > 60)
        {
            CancelInvoke("StartWave4");
            GameManager.Get.EndWave();
        }
    }

    public void StartWave5()
    {
        ufoCounter++;
        // leave some gaps
        if (ufoCounter % 10 <= 1) return;

        if (ufoCounter < 10)
        {
            SpawnUFO(Random.Range(3, 5), Path.Path1);
        }
        else if (ufoCounter < 15)
        {
            SpawnUFO(Random.Range(4, 5), Random.Range(0, 2) == 0 ? Path.Path1 : Path.Path2);
        }
        else
        {
            SpawnUFO(Random.Range(4, 6), Path.Path1);
        }

        if (ufoCounter > 20)
        {
            CancelInvoke("StartWave5");
            GameManager.Get.EndWave();
        }
    }

    public void StartWave6()
    {
        ufoCounter++;
        // leave some gaps
        if (ufoCounter % 10 <= 1) return;

        if (ufoCounter < 10)
        {
            SpawnUFO(Random.Range(4, 6), Random.Range(0, 2) == 0 ? Path.Path1 : Path.Path2);
        }
        else if (ufoCounter < 15)
        {
            SpawnUFO(Random.Range(4, 7), Random.Range(0, 2) == 0 ? Path.Path1 : Path.Path2);
        }
        else
        {
            SpawnUFO(Random.Range(5, 7), Random.Range(0, 2) == 0 ? Path.Path1 : Path.Path2);
        }

        if (ufoCounter > 20)
        {
            CancelInvoke("StartWave6");
            GameManager.Get.EndWave();
        }
    }

    public void StartWave7()
    {
        ufoCounter++;
        // leave some gaps
        if (ufoCounter % 10 <= 1) return;

        if (ufoCounter < 20)
        {
            SpawnUFO(Random.Range(3, 6), Random.Range(0, 2) == 0 ? Path.Path1 : Path.Path2);
        }
        else if (ufoCounter < 30)
        {
            SpawnUFO(Random.Range(5, 7), Random.Range(0, 2) == 0 ? Path.Path1 : Path.Path2);
        }
        else if (ufoCounter < 50)
        {
            SpawnUFO(Random.Range(3, 7), Random.Range(0, 2) == 0 ? Path.Path1 : Path.Path2);
        } else
        {
            SpawnUFO(6, Random.Range(0, 2) == 0 ? Path.Path1 : Path.Path2);
        }

        if (ufoCounter > 60)
        {
            CancelInvoke("StartWave7");
            GameManager.Get.EndWave();
        }
    }

    public void StartWave8()
    {
        ufoCounter++;
        // leave some gaps
        if (ufoCounter % 15 <= 1) return;

        if (ufoCounter < 20)
        {
            SpawnUFO(Random.Range(3, 7), Random.Range(0, 2) == 0 ? Path.Path1 : Path.Path2);
        }
        else if (ufoCounter < 30)
        {
            SpawnUFO(Random.Range(6, 7), Random.Range(0, 2) == 0 ? Path.Path1 : Path.Path2);
        }
        else if (ufoCounter < 50)
        {
            SpawnUFO(Random.Range(4, 7), Random.Range(0, 2) == 0 ? Path.Path1 : Path.Path2);
        }
        else
        {
            SpawnUFO(6, Random.Range(0, 2) == 0 ? Path.Path1 : Path.Path2);
        }

        if (ufoCounter > 70)
        {
            CancelInvoke("StartWave8");
            GameManager.Get.EndWave();
        }
    }
}
