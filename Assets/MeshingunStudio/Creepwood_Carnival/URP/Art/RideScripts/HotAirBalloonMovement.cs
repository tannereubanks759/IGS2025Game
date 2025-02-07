using UnityEngine;

public class HotAirBalloonMovement : MonoBehaviour
{
    public float floatAmplitudeMin = 2f; // Minimum vertical oscillation amplitude
    public float floatAmplitudeMax = 10f; // Maximum vertical oscillation amplitude
    public float floatFrequencyMin = 0.2f; // Minimum oscillation frequency
    public float floatFrequencyMax = 1f; // Maximum oscillation frequency
    public float circleRadiusMin = 5f; // Minimum circular motion radius
    public float circleRadiusMax = 20f; // Maximum circular motion radius
    public float circleSpeedMin = 0.5f; // Minimum circular motion speed
    public float circleSpeedMax = 2f; // Maximum circular motion speed

    private Vector3 startPosition; // Initial position of the balloon
    private float floatAmplitude; // Vertical oscillation amplitude
    private float floatFrequency; // Vertical oscillation frequency
    private float circleRadius; // Circular motion radius
    private float circleSpeed; // Circular motion speed

    void Start()
    {
        startPosition = transform.position; // Store the initial position
        // Randomly select values for the motion parameters
        floatAmplitude = Random.Range(floatAmplitudeMin, floatAmplitudeMax);
        floatFrequency = Random.Range(floatFrequencyMin, floatFrequencyMax);
        circleRadius = Random.Range(circleRadiusMin, circleRadiusMax);
        circleSpeed = Random.Range(circleSpeedMin, circleSpeedMax);
    }

    void Update()
    {
        // Vertical oscillation using a sine wave
        float floatOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

        // Circular motion
        float x = Mathf.Cos(Time.time * circleSpeed) * circleRadius;
        float z = Mathf.Sin(Time.time * circleSpeed) * circleRadius;

        // Update the position
        transform.position = new Vector3(startPosition.x + x, startPosition.y + floatOffset, startPosition.z + z);
    }
}
