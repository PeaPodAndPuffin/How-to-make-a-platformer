using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    public string sceneName;

    private void Start()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            SceneManager.LoadScene(sceneName);
        }



    }
}
