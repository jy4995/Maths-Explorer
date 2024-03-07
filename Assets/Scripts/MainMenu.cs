using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject GreaterOrSmaller;
    public GameObject Arrange;
    public GameObject Composing;
    public GameObject ExitButton;
  
    public void StartGS()
    {
        ScenesManager.Instance.LoadGS();
    }

    public void StartA()
    {
        ScenesManager.Instance.LoadA();
    }

    public void StartC()
    {
        ScenesManager.Instance.LoadC();
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
