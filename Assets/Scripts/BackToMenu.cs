using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{
    public GameObject BackButton;

    public void Back()
    {
        ScenesManager.Instance.LoadMain();
    }
}
