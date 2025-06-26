using UnityEngine;
using System.Collections;

public class RandomSpawnerArea : MonoBehaviour
{
    public Vector2 size = new Vector2(5f, 5f);
    public PathData pathData;
    public ProblemsData problemsData;

    public Wave[] waves;
    public float timeBetweenWaves = 5f;
    public bool flipEnemies = false;

    private int currentWaveIndex = 0;
    private bool isSpawning = false;

    private void Update()
    {
        if (!isSpawning && currentWaveIndex < waves.Length)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        isSpawning = true;

        for (int i = 0; i < wave.count; i++)
        {
            // Randomize point to spawn a monster
            Vector2 center = transform.position;
            float x = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
            float y = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
            Vector2 spawnPosition = new Vector2(x, y);

            // Spawn the monster and set path to move
            GameObject spawnedObject = Instantiate(wave.enemyPrefab, spawnPosition, Quaternion.identity);
            spawnedObject.GetComponent<EnemyPathMovement>().pathData = pathData;

            // Flip sprite
            if (flipEnemies)
            {
                spawnedObject.GetComponent<Enemy>().Flip();
            }

            // Randomize problem and answer
            TextBallon textBallon = spawnedObject.GetComponent<Enemy>().textBallon;

            if (textBallon != null)
            {
                var generated = ProblemGenerator.GenerateRandomLinearEquationProblem();
                textBallon.problemsData = problemsData;
                textBallon.questionText = generated.problemText;
                textBallon.answers = generated.answers;
                textBallon.correctAnswerIndex = generated.correctAnswerIndex;
                textBallon.SetQuestionAndAnswers();
                textBallon.SetOptionListeners();
            }

            yield return new WaitForSeconds(wave.spawnInterval);
        }

        currentWaveIndex++;
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
