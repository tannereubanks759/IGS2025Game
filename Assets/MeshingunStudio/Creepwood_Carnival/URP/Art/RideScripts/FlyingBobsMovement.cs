using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBobsMovement : MonoBehaviour
{
    public Transform[] vehicles; // Add the Transforms of the vehicles here
    public float lerpDuration = 2f; // Duration to transition between rotations
    private Quaternion[][] allTargetRotations; // All target rotations for each vehicle
    private Quaternion[] initialRotations; // Stores the initial rotations of the vehicles
    private float[] lerpTimers; // Individual timer for each vehicle
    private int[] currentTargetIndex; // Keeps track of the current target index for each vehicle

    void Start()
    {
        // Save initial rotations
        SaveInitialRotations();

        // Set target rotations for all vehicles
        SetAllTargetRotations();

        // Initialize timers and target indices
        lerpTimers = new float[vehicles.Length];
        currentTargetIndex = new int[vehicles.Length];
    }

    void Update()
    {
        for (int i = 0; i < vehicles.Length; i++)
        {
            // Update the timer
            lerpTimers[i] += Time.deltaTime;

            // Apply the LERP operation
            vehicles[i].rotation = Quaternion.Lerp(
                initialRotations[i],
                allTargetRotations[i][currentTargetIndex[i]],
                lerpTimers[i] / lerpDuration
            );

            // If LERP is complete, move to the next target
            if (lerpTimers[i] >= lerpDuration)
            {
                // Reset the timer
                lerpTimers[i] = 0f;

                // Update the next target
                initialRotations[i] = vehicles[i].rotation; // Update the starting rotation
                currentTargetIndex[i] = (currentTargetIndex[i] + 1) % vehicles.Length; // Cycle through targets
            }
        }
    }

    void SaveInitialRotations()
    {
        initialRotations = new Quaternion[vehicles.Length];
        for (int i = 0; i < vehicles.Length; i++)
        {
            initialRotations[i] = vehicles[i].rotation; // Save the initial rotations of the vehicles
        }
    }

    void SetAllTargetRotations()
    {
        allTargetRotations = new Quaternion[vehicles.Length][];
        for (int i = 0; i < vehicles.Length; i++)
        {
            allTargetRotations[i] = new Quaternion[vehicles.Length];
            for (int j = 0; j < vehicles.Length; j++)
            {
                int targetIndex = (i + j + 1) % vehicles.Length; // Calculate the next target for each vehicle
                allTargetRotations[i][j] = vehicles[targetIndex].rotation;
            }
        }
    }
}
