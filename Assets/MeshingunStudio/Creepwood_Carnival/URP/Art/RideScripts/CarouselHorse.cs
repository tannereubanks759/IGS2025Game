using UnityEngine;
using System.Collections;

public class CarouselHorse : MonoBehaviour
{
    public Transform centerPoint; // The central object around which the horses will rotate
    public float maxRotationSpeed = 10f; // Maximum rotation speed
    public float accelerationTime = 3f;  // Acceleration time
    public float rotationTime = 5f;      // Time rotating at full speed
    public float decelerationTime = 3f;  // Deceleration time
    public float pauseTime = 2f;         // Pause time after rotating
    public float verticalMoveAmount = 0.5f; // Vertical movement distance
    public float minVerticalSpeed = 1f; // Minimum vertical speed
    public float maxVerticalSpeed = 3f; // Maximum vertical speed
    public float verticalMoveDuration = 1f; // Vertical acceleration duration
    public float verticalDecelerationTime = 0.5f; // Vertical deceleration duration

    private float currentRotationSpeed = 0f; // Current rotation speed
    private bool isRotating = false;
    private bool isVerticalMoveActive = true;
    private float initialYPosition;
    private float verticalSpeed;
    private float verticalOffset;
    private float currentVerticalMoveAmount = 0f; // Current amplitude value for smooth movement
    private float currentVerticalSpeed = 0f; // Vertical speed

    void Start()
    {
        initialYPosition = transform.localPosition.y;
        verticalSpeed = Random.Range(minVerticalSpeed, maxVerticalSpeed);
        verticalOffset = Random.Range(0f, Mathf.PI * 2);

        StartCoroutine(ActivateVerticalMoveSmooth()); // Dikey hareketi hemen baþlat
        StartCoroutine(RotationCycle());
    }

    void Update()
    {
        if (isRotating)
        {
            transform.RotateAround(centerPoint.position, Vector3.up, -currentRotationSpeed * Time.deltaTime);
        }

        if (isVerticalMoveActive)
        {
            currentVerticalMoveAmount = Mathf.Lerp(0, verticalMoveAmount, currentVerticalSpeed);
            float newYPosition = initialYPosition + Mathf.Sin(Time.time * verticalSpeed + verticalOffset) * currentVerticalMoveAmount;
            transform.localPosition = new Vector3(transform.localPosition.x, newYPosition, transform.localPosition.z);
        }
    }

    IEnumerator RotationCycle()
    {
        while (true)
        {
            yield return StartCoroutine(ChangeSpeed(0, maxRotationSpeed, accelerationTime));

            isVerticalMoveActive = true; // Dikey hareketi aktif tut
            yield return new WaitForSeconds(rotationTime);

            yield return StartCoroutine(ChangeSpeed(maxRotationSpeed, 0, decelerationTime));

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

        if (endSpeed == 0)
        {
            isRotating = false;
        }
    }

    IEnumerator ActivateVerticalMoveSmooth()
    {
        isVerticalMoveActive = true;
        float elapsedTime = 0f;

        while (elapsedTime < verticalMoveDuration)
        {
            currentVerticalSpeed = Mathf.Lerp(0, 1, elapsedTime / verticalMoveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentVerticalSpeed = 1f;
    }

    IEnumerator DeactivateVerticalMoveSmooth()
    {
        float elapsedTime = 0f;
        float initialVerticalSpeed = currentVerticalSpeed;

        while (elapsedTime < verticalDecelerationTime)
        {
            currentVerticalSpeed = Mathf.Lerp(initialVerticalSpeed, 0, elapsedTime / verticalDecelerationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentVerticalSpeed = 0f;
        isVerticalMoveActive = false;
    }
}
