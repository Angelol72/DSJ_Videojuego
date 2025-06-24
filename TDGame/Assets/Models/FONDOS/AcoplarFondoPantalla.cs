using UnityEngine;

public class AcoplarFondoPantalla : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("No se encontró SpriteRenderer en este GameObject. Asegúrate de que tu imagen sea un Sprite.");
            return;
        }

        // Obtiene el tamaño de la pantalla en unidades de Unity
        Camera mainCamera = Camera.main;
        float screenHeight = mainCamera.orthographicSize * 2;
        float screenWidth = screenHeight * mainCamera.aspect;

        // Obtiene el tamaño original del sprite
        float spriteWidth = spriteRenderer.sprite.bounds.size.x;
        float spriteHeight = spriteRenderer.sprite.bounds.size.y;

        // Calcula la escala para que el sprite CUBRA completamente la pantalla
        // (puede recortar bordes si la relación de aspecto de la imagen no coincide con la pantalla)
        float scaleX = screenWidth / spriteWidth;
        float scaleY = screenHeight / spriteHeight;

        transform.localScale = new Vector3(Mathf.Max(scaleX, scaleY), Mathf.Max(scaleX, scaleY), 1);

        // Centra el fondo con la cámara (opcional, si no está en 0,0)
        transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, transform.position.z);
    }
}