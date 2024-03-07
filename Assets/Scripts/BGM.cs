using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour
{
    private static BGM instance;
    public AudioSource bgm;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(StartMusic());
        }
        else
        {
            Destroy(gameObject);
        }
    }


    IEnumerator StartMusic()
    {
        yield return new WaitForSeconds(4);
        bgm.Play();
    }
}

