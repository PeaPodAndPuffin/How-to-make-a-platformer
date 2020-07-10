using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{

    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    public void LoadLevel(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
