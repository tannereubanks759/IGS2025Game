using UnityEngine;

public class BalloonTowerMovement : MonoBehaviour
{
    public Transform center; // Center point
    public float maxRotationSpeed = 20f; // Maximum rotation speed
    public float acceleration = 5f; // Acceleration rate
    public float deceleration = 5f; // Deceleration rate
    public float maxSpeedDuration = 2f; // Duration at maximum speed
    public float waitAtRestTime = 2f; // Rest time after deceleration

    public float riseHeight = 10f; // Height of rise along the Y-axis
    public float riseDuration = 2f; // Duration of rise along the Y-axis
    public float descendDuration = 2f; // Duration of descent along the Y-axis
    public float riseDelay = 1f; // Delay before starting the rise movement along the Y-axis

    public float maxTiltAngle = 60f; // Maximum tilt angle on the Y-axis
    public float minTiltAngle = 20f; // Minimum tilt angle
    public float minSwingFrequency = 0.5f; // Minimum swing frequency
    public float maxSwingFrequency = 1f; // Maximum swing frequency
    public bool rotateAroundCenter = true; // Whether to rotate around the center
    public bool rotateOnOwnAxis = true; // Whether to rotate on its own axis

    private float currentSpeed = 0f; // Current speed
    private bool isAccelerating = false; // Acceleration state
    private bool isDescending = false; // Descent state
    private bool isAtRest = false; // Rest state
    private bool rising = false; // Rising state
    private float maxSpeedTimeElapsed = 0f; // Time elapsed at maximum speed
    private float initialY; // Initial Y position
    private float waitTimeElapsed = 0f; // Time elapsed during rest
    private float swingFrequency; // Swing frequency
    private float swingTimer = 0f; // Timer for tracking swing duration
    private float tiltAngle; // Tilt angle
    private float riseTimer = 0f; // Timer for rise duration
    private float descendTimer = 0f; // Timer for descent duration
    private float riseDelayTimer = 0f; // Timer for the delay before starting the rise movement
    private bool waitingForRise = true; // Waiting state before the first rise
    private float initialTiltAngle; // Initial tilt angle

    void Start()
    {
        initialY = transform.position.y; // Set the initial Y position
        initialTiltAngle = transform.localRotation.eulerAngles.y; // Set the initial tilt angle
        swingFrequency = Random.Range(minSwingFrequency, maxSwingFrequency);
    }

    void Update()
    {
        if (waitingForRise)
        {
            // Delay before the first rise
            riseDelayTimer += Time.deltaTime;
            if (riseDelayTimer >= riseDelay)
            {
                waitingForRise = false;
                rising = true;
                riseDelayTimer = 0f;
            }
            return;
        }

        if (rising)
        {
            // Smooth rise movement along the Y-axis
            riseTimer += Time.deltaTime / riseDuration;
            float interpolatedOffset = Mathf.Lerp(0, riseHeight, Mathf.SmoothStep(0f, 1f, riseTimer));
            transform.position = new Vector3(transform.position.x, initialY + interpolatedOffset, transform.position.z);

            if (riseTimer >= 1f)
            {
                // When the rise is complete
                rising = false;
                isAccelerating = true;
                maxSpeedTimeElapsed = 0f;
                riseTimer = 0f;
            }
        }
        else if (isAtRest)
        {
            // Rest time
            waitTimeElapsed += Time.deltaTime;
            if (waitTimeElapsed >= waitAtRestTime)
            {
                isAtRest = false;
                isDescending = true;
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
            }
        }
        else if (!isDescending && maxSpeedTimeElapsed < maxSpeedDuration)
        {
            // Duration at maximum speed
            maxSpeedTimeElapsed += Time.deltaTime;
        }
        else if (!isAccelerating && !isDescending)
        {
            // Deceleration
            currentSpeed -= deceleration * Time.deltaTime;
            if (currentSpeed <= 0)
            {
                currentSpeed = 0;
                isAtRest = true;
                waitTimeElapsed = 0f;
            }
        }

        // Rotation around the center
        if (!rising && rotateAroundCenter)
        {
            transform.RotateAround(center.position, Vector3.up, currentSpeed * Time.deltaTime);
        }

        // Smooth rotation on its own axis with start and stop effects
        if (!rising && rotateOnOwnAxis && currentSpeed > 0)
        {
            swingTimer += Time.deltaTime * Mathf.Lerp(minSwingFrequency, maxSwingFrequency, currentSpeed / maxRotationSpeed);
            float tiltAngleMultiplier = Mathf.Sin(swingTimer);
            float dynamicMaxTilt = Mathf.Lerp(minTiltAngle, maxTiltAngle, Mathf.SmoothStep(0f, 1f, currentSpeed / maxRotationSpeed));

            tiltAngle = initialTiltAngle + (tiltAngleMultiplier * dynamicMaxTilt);
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, tiltAngle, transform.localRotation.eulerAngles.z);
        }

        if (isDescending)
        {
            // Smooth descent movement along the Y-axis
            descendTimer += Time.deltaTime / descendDuration;
            float interpolatedOffset = Mathf.Lerp(riseHeight, 0, Mathf.SmoothStep(0f, 1f, descendTimer));
            transform.position = new Vector3(transform.position.x, initialY + interpolatedOffset, transform.position.z);

            if (descendTimer >= 1f)
            {
                // When the descent is complete
                transform.position = new Vector3(transform.position.x, initialY, transform.position.z);
                isDescending = false;
                waitingForRise = true;
                waitTimeElapsed = 0f;
                descendTimer = 0f;
            }
        }
    }
}
