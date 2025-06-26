using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;
using System;

public class TextBallon : MonoBehaviour
{
    public string questionText;
    public string[] answers;
    public int correctAnswerIndex;
    public GameObject problemText;
    public GameObject options;
    public Enemy enemy;
    public EnemyPathMovement enemyPathMovement;

    // ProblemaData reference
    public ProblemsData problemsData;

    // Event Handlers
    public EventHandler onCorrectAnswerEvent;
    public EventHandler onWrongAnswerEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetQuestionAndAnswers(); // Set the question and answers at the start
        SetOptionListeners(); // Set listeners for the options
    }

    public void chargeNewProblem()
    {
        if (problemsData == null)
        {
            Debug.LogError("ProblemsData is not set");
            return;
        }

        // Generate a new problem
        var generated = ProblemGenerator.GenerateRandomLinearEquationProblem();

        // Set the question text and answers
        questionText = generated.problemText;
        answers = generated.answers;
        correctAnswerIndex = generated.correctAnswerIndex;

        // Update the problem text and options
        SetQuestionAndAnswers();
        SetOptionListeners();
    }

    // set the question text and answers to the problem text and options
    public void SetQuestionAndAnswers()
    {
        //Debug.Log("Setting question and answers...");
        //Debug.Log($"Question: {questionText}");
        //Debug.Log($"Answers: {string.Join(", ", answers)}");

        if (problemText != null)
        {
            TMP_Text msg = problemText.transform
                .GetChild(0) // Button
                .GetChild(0) // Text
                .GetComponent<TMP_Text>();

            msg.text = questionText;
        }

        if (options != null)
        {
            for (int i = 0; i < answers.Length; i++)
            {
                TMP_Text msg = options.transform
                    .GetChild(i) // Canvas
                    .GetChild(0) // Button 
                    .GetChild(0) // Text
                    .GetComponent<TMP_Text>();

                msg.text = answers[i];
            }
        }
    }

    // set event listeners for the options, correct answer and wrong answers
    // use the correct answer index to determine if the answer is correct or not
    public void SetOptionListeners()
    {
        for (int i = 0; i < options.transform.childCount; i++)
        {

            int index = i; // Capture the current index

            // Get just the button component
            GameObject btn = options.transform
                .GetChild(index) // Canvas
                .GetChild(0) // Button
                .gameObject;

            btn.GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners(); // Clear existing listeners

            if (i == correctAnswerIndex)
            {
                btn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
                {
                    ShowMessageOnCorrectAnswer();
                    onCorrectAnswerEvent?.Invoke(this, EventArgs.Empty); // Invoke the event for correct answer
                });
            }
            else
            {
                btn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
                {
                    ShowMessageOnWrongAnswer();
                    onWrongAnswerEvent?.Invoke(this, EventArgs.Empty); // Invoke the event for wrong answer
                });
            }
        }
    }

    // Event on correct answer
    private void ShowMessageOnCorrectAnswer()
    {
        // Get the text component of the button problem
        TMP_Text msg = problemText.transform
            .GetChild(0) // Button
            .GetChild(0) // Text
            .GetComponent<TMP_Text>();
        msg.text = "Correct!";
    }

    // Event on wrong answer
    private void ShowMessageOnWrongAnswer()
    {
        // Get the text component of the button problem
        TMP_Text msg = problemText.transform
            .GetChild(0) // Button
            .GetChild(0) // Text
            .GetComponent<TMP_Text>();

        msg.text = "!";
        StartCoroutine(WaitAndResetText(msg));

        Debug.Log("Wrong answer selected!");
        // Add your logic for wrong answer here, e.g., show a message, update score, etc.
    }

    // Coroutine to wait and reset the text after a delay
    private System.Collections.IEnumerator WaitAndResetText(TMP_Text msg)
    {
        yield return new WaitForSeconds(enemyPathMovement.enrageDuration);
        msg.text = questionText; // Reset the text to the original question
    }

}
