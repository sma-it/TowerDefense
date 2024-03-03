using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public bool MenuScene;
    // Start is called before the first frame update
    void Start()
    {
        if (MenuScene)
        {
            SoundManager.Get.StartMenuMusic();
        } else
        {
            SoundManager.Get.StartGameMusic();
            GameManager.Get.StartGame();
        }
    }
}
