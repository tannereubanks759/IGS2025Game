using UnityEngine;

public class SwingMovement : MonoBehaviour
{
    public Transform center; // Center point
    public float maxRotationSpeed = 20f; // Maximum rotation speed
    public float acceleration = 5f; // Acceleration rate
    public float deceleration = 5f; // Deceleration rate
    public float maxSpeedDuration = 2f; // Duration at max speed
    public float waitAtRestTime = 2f; // Rest duration after stopping
    public float maxTiltAngle = 30f; // Maximum tilt angle on the X-axis
    public float maxYMovement = 5f; // Maximum movement distance on the Y-axis
    public float yMovementSpeed = 1f; // Speed of Y-axis movement
    public bool reverseRotation = false; // Control to reverse rotation direction

    private float currentSpeed = 0f; // Current speed
    private bool isAccelerating = true; // Acceleration state
    private bool isDecelerating = false; // Deceleration state
    private float maxSpeedTimeElapsed = 0f; // Time elapsed at max speed
    private float waitTimeElapsed = 0f; // Time elapsed during rest
    private bool isAtRest = false; // Rest state
    private float currentYPosition = 0f; // Current position on the Y-axis
    private Vector3 initialLocalPosition; // Initial local position

    void Start()
    {
        // Save the initial local position
        initialLocalPosition = transform.localPosition;
    }

    void Update()
    {
        if (isAtRest)
        {
            // Waiting at rest
            waitTimeElapsed += Time.deltaTime;
            if (waitTimeElapsed >= waitAtRestTime)
            {
                isAtRest = false;
                isAccelerating = true; // Start accelerating after rest
            }
        }
        else if (isAccelerating)
        {
            // Accelerate
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed >= maxRotationSpeed)
            {
                currentSpeed = maxRotationSpeed;
                isAccelerating = false; // Stop accelerating once max speed is reached
                maxSpeedTimeElapsed = 0f; // Reset time at max speed
            }
        }
        else if (!isDecelerating && maxSpeedTimeElapsed < maxSpeedDuration)
        {
            // Maintain max speed for the defined duration
            maxSpeedTimeElapsed += Time.deltaTime;
        }
        else if (!isAccelerating && !isDecelerating)
        {
            // Start decelerating
            currentSpeed -= deceleration * Time.deltaTime;
            if (currentSpeed <= 0)
            {
                currentSpeed = 0;
                isDecelerating = false; // Stop decelerating
                isAtRest = true; // Swing comes to rest
                waitTimeElapsed = 0f; // Reset rest time
            }
        }

        // Rotation direction based on reverseRotation flag
        float rotationDirection = reverseRotation ? 1f : -1f;

        // Swing rotation around the center point
        transform.RotateAround(center.position, Vector3.up, rotationDirection * currentSpeed * Time.deltaTime);

        // Tilt on the X-axis based on speed
        float tiltAngle = Mathf.Lerp(0f, maxTiltAngle, currentSpeed / maxRotationSpeed);
        transform.localRotation = Quaternion.Euler(tiltAngle, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);

        // Movement on the Y-axis (oscillation)
        float yMovement = Mathf.Lerp(0f, maxYMovement, currentSpeed / maxRotationSpeed);
        currentYPosition = Mathf.Lerp(currentYPosition, yMovement, yMovementSpeed * Time.deltaTime);

        // Apply local Y-axis movement
        transform.localPosition = new Vector3(transform.localPosition.x, initialLocalPosition.y + currentYPosition, transform.localPosition.z);
    }
}
