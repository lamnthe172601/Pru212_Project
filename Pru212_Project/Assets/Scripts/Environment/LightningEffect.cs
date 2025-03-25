using System.Collections;
using UnityEngine;

public class LightningEffect : MonoBehaviour
{
    public GameObject lightningPrefab;
    public float minDelay = 1f, maxDelay = 5f;

    private Camera mainCamera;
    private Coroutine thunderCoroutine;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        // Start the thunder routine when the object is enabled
        thunderCoroutine = StartCoroutine(ThunderRoutine());
    }

    private void OnDisable()
    {
        // Stop the coroutine when the object is disabled
        if (thunderCoroutine != null)
        {
            StopCoroutine(thunderCoroutine);
            thunderCoroutine = null;
        }
    }

    private IEnumerator ThunderRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            // Get random position within camera view
            Vector2 spawnPosition = GetRandomPositionInView();

            // Instantiate lightning at the random position
            GameObject lightning = Instantiate(lightningPrefab, spawnPosition, Quaternion.identity);
            // Shake the camera.
            CameraShake.instance.ShakeIt(0.1f, 0.2f);
            Destroy(lightning, 1f); // Destroy after animation plays
        }
    }

    private Vector2 GetRandomPositionInView()
    {
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Get random X and Y position within camera view
        float randomX = Random.Range(mainCamera.transform.position.x - cameraWidth / 2,
                                     mainCamera.transform.position.x + cameraWidth / 2);

        float randomY = Random.Range(mainCamera.transform.position.y - cameraHeight / 2,
                                     mainCamera.transform.position.y + cameraHeight / 2);

        return new Vector2(randomX, randomY);
    }
}
