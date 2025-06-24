using System.Collections.Generic;
using UnityEngine;

public static class ProblemGenerator
{
    public struct GeneratedProblem
    {
        public string problemText;
        public string[] answers;
        public int correctAnswerIndex;
    }

    public static GeneratedProblem GenerateRandomProblem(ProblemsData problemsData)
    {
        // Select a random problem
        int problemIndex = Random.Range(0, problemsData.problems.Length);
        var selectedProblem = problemsData.problems[problemIndex];

        List<string> options = new List<string>();
        int correctIndex = Random.Range(0, 3);

        int correctNumber;
        bool isNumber = int.TryParse(selectedProblem.answerText, out correctNumber);

        // Generate answers
        for (int i = 0; i < 3; i++)
        {
            if (i == correctIndex)
            {
                options.Add(selectedProblem.answerText);
            }
            else if (isNumber)
            {
                int delta;
                int wrongAnswer;
                do
                {
                    delta = Random.Range(-5, 6); // Range [-5, 5]
                } while (delta == 0 || options.Contains((correctNumber + delta).ToString()));

                wrongAnswer = correctNumber + delta;
                options.Add(wrongAnswer.ToString());
            }
            else
            {
                // Gather all possible correct answers except the current one and already used
                List<string> otherAnswers = new List<string>();
                for (int j = 0; j < problemsData.problems.Length; j++)
                {
                    string candidate = problemsData.problems[j].answerText;
                    if (candidate != selectedProblem.answerText && !options.Contains(candidate))
                        otherAnswers.Add(candidate);
                }
                if (otherAnswers.Count > 0)
                {
                    string wrong = otherAnswers[Random.Range(0, otherAnswers.Count)];
                    options.Add(wrong);
                }
                else
                {
                    options.Add("N/A");
                }
            }
        }

        return new GeneratedProblem
        {
            problemText = selectedProblem.problemText,
            answers = options.ToArray(),
            correctAnswerIndex = correctIndex
        };
    }
}