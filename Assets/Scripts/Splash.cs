using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    public GameObject splashBack;
    public GameObject splashText;
    private static bool StartUp = true;
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            StartCoroutine(SplashEnd());
        }
        else
        {
            if (StartUp == true)
            {
                StartUp = false;
                StartCoroutine(SplashEnd());
            }
            else
            {
                splashBack.SetActive(false);
                splashText.SetActive(false);
            }
        }

    }


    IEnumerator SplashEnd()
    {
        yield return new WaitForSeconds(5);
        splashBack.SetActive(false);
        splashText.SetActive(false);
    }
}

