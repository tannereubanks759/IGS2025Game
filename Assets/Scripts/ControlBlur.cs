using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ControlBlur : MonoBehaviour
{
    public Volume globalVolume;

    public GameObject pauseMenu;

    public float focusDistance;

    public GameObject[] reticle;

    private void Start()
    {
        globalVolume = GetComponent<Volume>();
    }

    public void ToggleBackgroundBlur()
    {
        if (pauseMenu.activeSelf)
        {
            globalVolume.priority = 3;

            globalVolume.weight = 1.0f;

            foreach (GameObject r in reticle)
            {
                r.SetActive(false);
            }
        }
        else
        {
            globalVolume.priority = 0;

            globalVolume.weight = 0f;

            foreach (GameObject r in reticle)
            {
                r.SetActive(true);
            }
        }
    }
}
