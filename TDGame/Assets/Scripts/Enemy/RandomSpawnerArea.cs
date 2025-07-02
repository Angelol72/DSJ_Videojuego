using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public bool isFinished = false;

    private void Update()
    {
        if (!isSpawning && currentWaveIndex < waves.Length)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
        }
    }

    private void OnEnable()
    {
        SpawnerManager.Instance?.RegisterSpawner(this);
    }

    private void OnDisable()
    {
        SpawnerManager.Instance?.UnregisterSpawner(this);
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

            //yield return new WaitForSeconds(wave.spawnInterval);

            float intervalTimer = 0f;
            while (intervalTimer < wave.spawnInterval && SpawnerManager.Instance != null && SpawnerManager.Instance.ActiveEnemyCount > 0)
            {
                intervalTimer += Time.deltaTime;
                yield return null;
            }
        }

        currentWaveIndex++;

        // Wait N seconds until next wave or spawn if number of enemies is zero
        float timer = 0f;
        while (timer < timeBetweenWaves && SpawnerManager.Instance != null && SpawnerManager.Instance.ActiveEnemyCount > 0)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        //isSpawning = false; 
        //yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = false;

        // Marcar como terminado si ya no quedan más oleadas
        if (currentWaveIndex >= waves.Length)
        {
            isFinished = true;
        }
    }

    public void ResetSpawner()
    {
        currentWaveIndex = 0;
        isSpawning = false;
        isFinished = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, size);
    }

}
