using UnityEngine;
using System.Collections;

public class FerrisWheelEngine : MonoBehaviour
{
    public float maxRotationSpeed = 10f; // Maximum rotation speed
    public float accelerationTime = 3f;  // Time to accelerate
    public float rotationTime = 5f;      // Time to rotate at full speed
    public float decelerationTime = 3f;  // Time to decelerate
    public float pauseTime = 2f;         // Pause time after rotation

    private float currentRotationSpeed = 0f; // Current rotation speed
    private bool isRotating = false;

    void Start()
    {
        // Start the rotation cycle coroutine
        StartCoroutine(RotationCycle());
    }

    void Update()
    {
        // Perform the engine's rotation
        if (isRotating)
        {
            transform.Rotate(Vector3.right, currentRotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator RotationCycle()
    {
        while (true)
        {
            // Acceleration
            yield return StartCoroutine(ChangeSpeed(0, maxRotationSpeed, accelerationTime));

            // Constant speed rotation
            yield return new WaitForSeconds(rotationTime);

            // Deceleration
            yield return StartCoroutine(ChangeSpeed(maxRotationSpeed, 0, decelerationTime));

            // Pause
            yield return new WaitForSeconds(pauseTime);
        }
    }

    IEnumerator ChangeSpeed(float startSpeed, float endSpeed, float duration)
    {
        isRotating = true;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            currentRotationSpeed = Mathf.Lerp(startSpeed, endSpeed, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentRotationSpeed = endSpeed;
        if (endSpeed == 0) isRotating = false;
    }
}
