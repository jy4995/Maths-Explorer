using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreaterSmaller : MonoBehaviour
{
    public GameObject answerGYellow; // Yellow is waiting
    public GameObject answerGGreen; // Green is correct
    public GameObject answerGRed; // Red is wrong

    public GameObject answerSYellow; // Yellow is waiting
    public GameObject answerSGreen; // Green is correct
    public GameObject answerSRed; // Red is wrong

    public GameObject questionL;
    public GameObject questionR;
    
    public GameObject answerG;
    public GameObject answerS;

    public AudioSource correctFX;
    public AudioSource wrongFX;

    public GameObject currentScore;
    public int scoreValue;
    public int bestScore;
    public GameObject bestDisplay;
    public static bool displayingQuestion = false;

    private void Start()
    {
        displayingQuestion = false;
        //PlayerPrefs.SetInt("BestScoreGS", 0);
        bestScore = PlayerPrefs.GetInt("BestScoreGS");
        bestDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Best: " + bestScore;
    }

    private void Update()
    {
        currentScore.GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + scoreValue;
        StartCoroutine(PushTextOnScreen());
    }
    public void AnswerG()
    {
        if (int.Parse(questionL.GetComponent<TMPro.TextMeshProUGUI>().text) > int.Parse(questionR.GetComponent<TMPro.TextMeshProUGUI>().text))
        {
            answerGYellow.SetActive(false);
            answerGGreen.SetActive(true);
            correctFX.Play();
            scoreValue += 1;
        }
        else
        {
            answerGYellow.SetActive(false);
            answerGRed.SetActive(true);
            wrongFX.Play();
            scoreValue = 0;
        }
        answerG.GetComponent<Button>().enabled = false;
        answerS.GetComponent<Button>().enabled = false;
        StartCoroutine(NextQuestion());

    }

    public void AnswerS()
    {
        if (int.Parse(questionL.GetComponent<TMPro.TextMeshProUGUI>().text) < int.Parse(questionR.GetComponent<TMPro.TextMeshProUGUI>().text))
        {
            answerSYellow.SetActive(false);
            answerSGreen.SetActive(true);
            correctFX.Play();
            scoreValue += 1;
        }
        else
        {
            answerSYellow.SetActive(false);
            answerSRed.SetActive(true);
            wrongFX.Play();
            scoreValue = 0;
        }
        answerG.GetComponent<Button>().enabled = false;
        answerS.GetComponent<Button>().enabled = false;
        StartCoroutine(NextQuestion());
    }

    IEnumerator NextQuestion()
    {
        if (bestScore < scoreValue)
        {
            PlayerPrefs.SetInt("BestScoreGS", scoreValue);
            bestScore = scoreValue;
            bestDisplay.GetComponent<TMPro.TextMeshProUGUI>().text = "Best: " + scoreValue;
        }

        yield return new WaitForSeconds(0.5f);

        answerGYellow.SetActive(true);
        answerGGreen.SetActive(false);
        answerGRed.SetActive(false);
        answerSYellow.SetActive(true);
        answerSGreen.SetActive(false);
        answerSRed.SetActive(false);
        answerG.GetComponent<Button>().enabled = true;
        answerS.GetComponent<Button>().enabled = true;
        displayingQuestion = false;
    }

    IEnumerator PushTextOnScreen()
    {
        yield return new WaitForSeconds(0.1f);
        if (displayingQuestion == false)
        {
            displayingQuestion = true;
            questionL.GetComponent<TMPro.TextMeshProUGUI>().text = Random.Range(1, 100).ToString();
            questionR.GetComponent<TMPro.TextMeshProUGUI>().text = Random.Range(1, 100).ToString();
            while (questionL.GetComponent<TMPro.TextMeshProUGUI>().text == questionR.GetComponent<TMPro.TextMeshProUGUI>().text)
            {
                questionR.GetComponent<TMPro.TextMeshProUGUI>().text = Random.Range(1, 100).ToString();
            }
        }
    }
}
