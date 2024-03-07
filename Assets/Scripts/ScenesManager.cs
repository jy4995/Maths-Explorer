using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public enum Scene
    {
        MainMenu,
        GreaterOrSmaller,
        Arrange,
        Composing
    }

    public void LoadMain()
    {
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }

    public void LoadGS()
    {
        SceneManager.LoadScene(Scene.GreaterOrSmaller.ToString());
    }

    public void LoadA()
    {
        SceneManager.LoadScene(Scene.Arrange.ToString());
    }

    public void LoadC()
    {
        SceneManager.LoadScene(Scene.Composing.ToString());
    }
}
