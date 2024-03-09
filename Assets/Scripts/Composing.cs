using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Composing : MonoBehaviour
{
    public GameObject Question;
    public GameObject[] Up;
    public GameObject[] Down;
    public GameObject SubmitButton;
    public GameObject ResetButton;
    public GameObject Complete;
    public GameObject CorrectPanel;
    public GameObject WrongPanel;

    public AudioSource correctFX;
    public AudioSource wrongFX;
    public AudioSource button1;
    public AudioSource button2;

    public GameObject currentScore;
    public GameObject bestDisplay;
    public int scoreValue;
    public int bestScore;

    public int[] QuestionNumArray = new int[5];
    public int[] PlayerNum = new int[2];

    public int QuestionNum;
    private bool NewQuestion = true;

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScoreCN");
        bestDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Best: " + bestScore;
    }
    void Update()
    {
        currentScore.GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + scoreValue;
        StartCoroutine(PushTextOnScreen());
        for (int i = 0; i < 2; i++)
        {
            if (PlayerNum[i] != 0)
            {
                Up[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = PlayerNum[i].ToString();
                Up[i].GetComponent<Button>().enabled = true;
            }
            else
            {
                Up[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "";
            }
            if (Up[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text == "")
            {
                Up[i].GetComponent<Button>().enabled = false;
            }
        }
    }

    public void DownToUp(int ButtonId)
    {
        for (int i = 0; i < 2; i++)
        {
            if (PlayerNum[i] == 0)
            {
                button1.Play();
                PlayerNum[i] = int.Parse(Down[ButtonId].GetComponentInChildren<TMPro.TextMeshProUGUI>().text);
                Down[ButtonId].SetActive(false);
                break;
            }
        }
    }

    public void UpToDown(int ButtonId)
    {
        int num = int.Parse(Up[ButtonId].GetComponentInChildren<TMPro.TextMeshProUGUI>().text);
        for (int i = 0; i < 2; i++)
        {
            if (PlayerNum[i] == num)
            {
                button2.Play();
                PlayerNum[i] = 0;
                break;
            }
        }
        for (int i = 0; i < 5; i++)
        {
            if (int.Parse(Down[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text) == num)
            {
                Down[i].SetActive(true);
                break;
            }
        }
    }

    public void Submit()
    {
        if (!PlayerNum.Contains(0))
        {
            if (PlayerNum[0] + PlayerNum[1] == QuestionNum)
            {
                Complete.SetActive(true);
                CorrectPanel.SetActive(true);
                correctFX.Play();
                scoreValue += 1;
                StartCoroutine(HideComplete());
                NewQuestion = true;
            }
            else
            {
                Complete.SetActive(true);
                WrongPanel.SetActive(true);
                wrongFX.Play();
                scoreValue = 0;
                StartCoroutine(HideComplete());
                ResetAll();
            }
        }
        
    }

    public void ResetAll()
    {
        PlayerNum = new int[2];
        for (int i = 0; i < 5; i++)
        {
            Down[i].SetActive(true);
        }
    }

    IEnumerator PushTextOnScreen()
    {
        yield return new WaitForSeconds(0.1f);
        if (NewQuestion)
        {
            if (bestScore < scoreValue)
            {
                PlayerPrefs.SetInt("BestScoreCN", scoreValue);
                bestScore = scoreValue;
                bestDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Best: " + scoreValue;
            }
            NewQuestion = false;
            QuestionNum = Random.Range(10, 100);
            QuestionNumArray[0] = Random.Range(1, QuestionNum - 1);
            QuestionNumArray[1] = QuestionNum - QuestionNumArray[0];
            for (int i = 2; i < 5; i++)
            {
                int num = Random.Range(1, QuestionNum - 1);
                while (QuestionNumArray.Contains<int>(num))
                {
                    num = Random.Range(1, QuestionNum - 1);
                }
                QuestionNumArray[i] = num;
            }
            for (int i = 0; i < 5; i++)
            {
                int tmp = QuestionNumArray[i];
                int r = Random.Range(i, 5);
                QuestionNumArray[i] = QuestionNumArray[r];
                QuestionNumArray[r] = tmp;
            }
            for (int i = 0; i < 2; i++)
            {
                Up[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
            for (int i = 0; i < 5; i++)
            {
                Down[i].GetComponentInChildren<TextMeshProUGUI>().text = QuestionNumArray[i].ToString();
            }
            Question.GetComponent<TMPro.TextMeshProUGUI>().text = QuestionNum.ToString();
            ResetAll();
        }        
   }

    IEnumerator HideComplete()
    {
        yield return new WaitForSeconds(1);
        Complete.SetActive(false);
        CorrectPanel.SetActive(false);
        WrongPanel.SetActive(false);
    }
}
