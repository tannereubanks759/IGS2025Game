using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform camTransform;

    public float shakeAmount = 0.3f;
    public float shakeSpeed = 5f;

    private Vector3 originalPos;
    private Vector3 targetOffset;

    void OnEnable()
    {
        if (camTransform == null)
            camTransform = transform;

        originalPos = camTransform.localPosition;

    }

    void Update()
    {
        if (MinigunScript.isFiring)
        {
            // Pick a new random target occasionally
            if (targetOffset == Vector3.zero || Vector3.Distance(camTransform.localPosition, originalPos + targetOffset) < 0.05f)
            {
                targetOffset = Random.insideUnitSphere * shakeAmount;
                targetOffset.z = 0;
            }

            // Smoothly move toward that random point
            camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, originalPos + targetOffset, Time.deltaTime * shakeSpeed);

        }
        else
        {
            camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, originalPos, Time.deltaTime * shakeSpeed);
            targetOffset = Vector3.zero;
        }
    }

}
