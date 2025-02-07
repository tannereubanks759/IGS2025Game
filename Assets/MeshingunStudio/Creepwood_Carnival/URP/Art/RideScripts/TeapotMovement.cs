using UnityEngine;

public class TeapotMovement : MonoBehaviour
{
    public Transform center; // Center point
    public float maxRotationSpeed = 20f; // Maximum rotation speed
    public float acceleration = 5f; // Acceleration rate
    public float deceleration = 5f; // Deceleration rate
    public float maxSpeedDuration = 2f; // Duration at max speed
    public float waitAtRestTime = 2f; // Rest duration after stopping

    public float maxTiltAngle = 120f; // Maximum tilt angle on the Y-axis
    public float minTiltAngle = 60f; // Minimum tilt angle
    public float minSwingFrequency = 0.5f; // Minimum swing frequency
    public float maxSwingFrequency = 1.5f; // Maximum swing frequency

    public bool rotateOnOwnAxis = false; // Control for rotating on its own axis

    private float currentSpeed = 0f; // Current speed
    private bool isAccelerating = true; // Acceleration state
    private bool isDecelerating = false; // Deceleration state
    private float maxSpeedTimeElapsed = 0f; // Time elapsed at max speed
    private float waitTimeElapsed = 0f; // Time elapsed during rest
    private bool isAtRest = false; // Rest state

    private float swingTimer = 0f; // Timer for swing movement
    private float randomTiltAngle; // Randomly assigned maximum tilt angle
    private float randomSwingFrequency; // Randomly assigned swing frequency

    void Start()
    {
        // Assign a random tilt angle and swing frequency for each object
        randomTiltAngle = Random.Range(minTiltAngle, maxTiltAngle);
        randomSwingFrequency = Random.Range(minSwingFrequency, maxSwingFrequency);
    }

    void Update()
    {
        // Rotation around the center point
        if (isAtRest)
        {
            waitTimeElapsed += Time.deltaTime;
            if (waitTimeElapsed >= waitAtRestTime)
            {
                isAtRest = false;
                isAccelerating = true;
            }
        }
        else if (isAccelerating)
        {
            // Acceleration
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed >= maxRotationSpeed)
            {
                currentSpeed = maxRotationSpeed;
                isAccelerating = false;
                maxSpeedTimeElapsed = 0f;
            }
        }
        else if (!isDecelerating && maxSpeedTimeElapsed < maxSpeedDuration)
        {
            maxSpeedTimeElapsed += Time.deltaTime;
        }
        else if (!isAccelerating && !isDecelerating)
        {
            // Deceleration
            currentSpeed -= deceleration * Time.deltaTime;
            if (currentSpeed <= 0)
            {
                currentSpeed = 0;
                isDecelerating = false;
                isAtRest = true;
                waitTimeElapsed = 0f;
            }
        }

        // Rotation around the center
        transform.RotateAround(center.position, Vector3.up, currentSpeed * Time.deltaTime);

        // If rotating on its own axis is enabled
        if (rotateOnOwnAxis)
        {
            // Smooth swinging motion on the Y-axis
            swingTimer += Time.deltaTime * randomSwingFrequency; // Different frequency for each object
            float speedRatio = currentSpeed / maxRotationSpeed;
            float dynamicTiltAngle = Mathf.Lerp(0, randomTiltAngle, speedRatio); // Tilt angle proportional to speed
            float tiltAngle = Mathf.Sin(swingTimer) * dynamicTiltAngle;

            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, tiltAngle, transform.localRotation.eulerAngles.z);
        }
    }
}
