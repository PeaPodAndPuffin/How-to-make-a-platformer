using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject panel;

    public void LoadScene(string sceneName) {

        StartCoroutine(FadeIn(sceneName));

    }

    public void Quit() {
        Application.Quit();
    }

    IEnumerator FadeIn(string sceneName) {
        panel.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
