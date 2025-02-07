using UnityEngine;

public class BumperCarMovement : MonoBehaviour
{
    public Transform[] waypoints;  // Waypoints to follow along the spline
    public float speed = 5f;       // Movement speed of the car
    public float rotationSpeed = 5f; // Speed of rotation
    public float rotationSmoothing = 0.1f; // Rotation smoothing value (between 0 and 1)
    public float maxRotationSpeed = 10f;  // Maximum rotation speed (limits the car's rotation rate)

    private int currentWaypointIndex = 0; // Current waypoint index
    private Transform currentWaypoint;   // Current waypoint transform

    void Start()
    {
        if (waypoints.Length > 0)
        {
            currentWaypoint = waypoints[currentWaypointIndex];
        }
    }

    void Update()
    {
        // If there are waypoints
        if (waypoints.Length > 0)
        {
            MoveAlongPath();
        }
    }

    void MoveAlongPath()
    {
        // Move towards the target waypoint
        Vector3 targetPosition = currentWaypoint.position;

        // Move the car towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Switch to the next waypoint when approaching the target waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0; // Loop back to the start if at the end of the path
            }

            currentWaypoint = waypoints[currentWaypointIndex]; // Assign the new waypoint
        }

        // Smoothly rotate towards the target waypoint
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;

        // Use RotateTowards for smooth directional rotation
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, directionToTarget, rotationSpeed * Time.deltaTime, 0f);

        // Use Quaternion.Lerp for smoother transition in rotation
        Quaternion targetRotation = Quaternion.LookRotation(newDirection);

        // Smoothly interpolate towards the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSmoothing);

        // Apply rotation towards the target direction
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxRotationSpeed * Time.deltaTime);
    }
}
