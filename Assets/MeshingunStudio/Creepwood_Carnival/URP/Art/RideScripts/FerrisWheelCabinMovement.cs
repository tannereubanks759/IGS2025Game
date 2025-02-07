using UnityEngine;
using System.Collections;

public class FerrisWheelCabinMovement : MonoBehaviour
{
    public Transform centerPoint; // The center object around which the cabins will rotate
    public float maxRotationSpeed = 10f; // Maximum rotation speed
    public float swayAmount = 10f; // Sway angle (in degrees)
    public float minSwaySpeed = 1f; // Minimum sway speed
    public float maxSwaySpeed = 3f; // Maximum sway speed
    public float accelerationTime = 3f; // Acceleration time
    public float rotationTime = 5f; // Full speed rotation time
    public float decelerationTime = 3f; // Deceleration time
    public float pauseTime = 2f; // Pause time after rotation

    private Quaternion initialRotation;
    private float swaySpeed;
    private float currentRotationSpeed = 0f; // Current rotation speed
    private bool isRotating = false;

    void Start()
    {
        // Save the initial rotation of the cabin
        initialRotation = transform.rotation;

        // Set a random sway speed
        swaySpeed = Random.Range(minSwaySpeed, maxSwaySpeed);

        // Start the Coroutine that controls the rotation cycle
        StartCoroutine(RotationCycle());
    }

    void Update()
    {
        // Rotate the cabins around the center object
        if (isRotating)
        {
            transform.RotateAround(centerPoint.position, centerPoint.right, currentRotationSpeed * Time.deltaTime);
        }

        // Perform the swaying movement
        float swayAngle = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.rotation = initialRotation * Quaternion.Euler(swayAngle, 0, 0);
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
