using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Arrange : MonoBehaviour
{
    public GameObject Question;
    public GameObject[] Up;
    public GameObject[] Down;
    public GameObject SubmitButton;
    public GameObject ResetButton;
    public GameObject Complete;
    public GameObject CompleteText;

    public AudioSource correctFX;
    public AudioSource wrongFX;
    public AudioSource button1;
    public AudioSource button2;

    private int[] QuestionNum = new int[5];
    private int[] QuestionNumSorted = new int[5];
    private int[] PlayerNum = new int[5];

    private bool Ascending = true;
    private bool NewQuestion = true;


    void Update()
    {
        StartCoroutine(PushTextOnScreen());
        for (int i = 0; i < 5; i++)
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
        for (int i = 0; i < 5; i++)
        {
            if (PlayerNum[i] == 0)
            {
                PlayerNum[i] = int.Parse(Down[ButtonId].GetComponentInChildren<TMPro.TextMeshProUGUI>().text);
                button1.Play();
                break;
            }
        }
        Down[ButtonId].SetActive(false);
    }

    public void UpToDown(int ButtonId)
    {
        int num = int.Parse(Up[ButtonId].GetComponentInChildren<TMPro.TextMeshProUGUI>().text);
        for (int i = 0; i < 5; i++)
        {
            if (PlayerNum[i] == num)
            {
                PlayerNum[i] = 0;
                break;
            }
        }
        for (int i = 0; i < 5; i++)
        {
            if (int.Parse(Down[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text) == num)
            {
                button2.Play();
                Down[i].SetActive(true);
                break;
            }
        }
    }

    public void Submit()
    {
        if (!PlayerNum.Contains<int>(0))
        {
            if (CompareAnswer())
            {
                Color color;
                ColorUtility.TryParseHtmlString("#83FF65", out color);
                Complete.GetComponent<Image>().color = color;
                Complete.SetActive(true);
                CompleteText.SetActive(true);
                CompleteText.GetComponent<TMPro.TextMeshProUGUI>().text = "Correct Answer!\nNext Question!";
                correctFX.Play();
                StartCoroutine(HideComplete());
                NewQuestion = true;
            }
            else
            {
                Color color;
                ColorUtility.TryParseHtmlString("#E24564", out color);
                Complete.GetComponent<Image>().color = color;
                Complete.SetActive(true);
                CompleteText.SetActive(true);
                CompleteText.GetComponent<TMPro.TextMeshProUGUI>().text = "Wrong Answer!\nPlease try again!";
                wrongFX.Play();
                StartCoroutine(HideComplete());
                ResetAll();
            }
        }
        
    }

    public void ResetAll()
    {
        PlayerNum = new int[5];
        for (int i = 0; i < 5; i++)
        {
            Down[i].SetActive(true);
        }
    }

    public bool CompareAnswer()
    {
        bool same = true;
        for (int i = 0; i < 5; i++)
        {
            if (PlayerNum[i] != QuestionNumSorted[i])
            {
                same = false;
                break;
            }
        }
        return same;
    }

    IEnumerator PushTextOnScreen()
    {
        yield return new WaitForSeconds(0.1f);
        if (NewQuestion)
        {
            NewQuestion = false;
            Ascending = Random.Range(0, 2) == 0;
            for (int i = 0; i < 5; i++)
            {
                int num = Random.Range(1, 100);
                while (QuestionNum.Contains<int>(num))
                {
                    num = Random.Range(1, 100);
                }
                QuestionNum[i] = num;
            }
            QuestionNumSorted = (int[])QuestionNum.Clone();
            System.Array.Sort(QuestionNumSorted);
            if (!Ascending)
            {
                QuestionNumSorted = QuestionNumSorted.Reverse().ToArray();
                Question.GetComponent<TMPro.TextMeshProUGUI>().text = "Descending";
            }
            else
            {
                Question.GetComponent<TMPro.TextMeshProUGUI>().text = "Ascending";
            }

            for (int i = 0; i < 5; i++)
            {
                Up[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
                Down[i].GetComponentInChildren<TextMeshProUGUI>().text = QuestionNum[i].ToString();
            }

            ResetAll();
        }
    }

    IEnumerator HideComplete()
    {
        yield return new WaitForSeconds(1);
        Complete.SetActive(false);
        CompleteText.SetActive(false);
    }
}
