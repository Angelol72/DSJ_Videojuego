using UnityEngine;

[CreateAssetMenu(fileName = "ProblemsData", menuName = "Scriptable Objects/ProblemsData")]
public class ProblemsData : ScriptableObject
{
    [System.Serializable]
    public class Problem
    {
        public string problemText;
        public string answerText; // Text for the problem
    }

    public Problem[] problems; // List of ordered problems
}