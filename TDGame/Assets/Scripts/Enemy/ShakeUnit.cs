using UnityEngine;
using System.Collections;

public class ShakeUnit : MonoBehaviour
{
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.1f;

    private Vector3 originalPos;

    public void TriggerShake()
    {
        //StopAllCoroutines(); // Evita múltiples shakes simultáneos
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        originalPos = transform.localPosition;

        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
