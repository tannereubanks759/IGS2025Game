using UnityEngine;
using System.Collections;

public class CarouselMechanism : MonoBehaviour
{
    public Transform centerPoint; // The center object around which the mechanism will rotate
    public float maxRotationSpeed = 15f; // Maximum rotation speed
    public float accelerationTime = 3f;  // Time taken to accelerate
    public float rotationTime = 5f;      // Time spent rotating at full speed
    public float decelerationTime = 3f;  // Time taken to decelerate
    public float pauseTime = 2f;         // Pause time after stopping

    private float currentRotationSpeed = 0f; // Current rotation speed
    private bool isRotating = false;

    void Start()
    {
        // Start the coroutine that manages the rotation cycle
        StartCoroutine(RotationCycle());
    }

    void Update()
    {
        // Rotate the mechanism around the center point in the reverse direction
        if (isRotating)
        {
            transform.RotateAround(centerPoint.position, Vector3.up, -currentRotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator RotationCycle()
    {
        while (true)
        {
            // Acceleration phase
            yield return StartCoroutine(ChangeSpeed(0, maxRotationSpeed, accelerationTime));

            // Rotation at constant speed
            yield return new WaitForSeconds(rotationTime);

            // Deceleration phase
            yield return StartCoroutine(ChangeSpeed(maxRotationSpeed, 0, decelerationTime));

            // Pause phase
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
