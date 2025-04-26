using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ControlBlur : MonoBehaviour
{
    public Volume globalVolume;
    private DepthOfField dof;

    private bool toggleDof;
    public float focusDistance;

    private void Start()
    {
        globalVolume = GetComponent<Volume>();

        toggleDof = false;
    }

    public void ToggleBackgroundBlur()
    {
        toggleDof = !toggleDof;

        if (globalVolume.profile.TryGet(out dof))
        {
            if (toggleDof)
            {
                globalVolume.priority = 3;

                globalVolume.weight = 1.0f;

                dof.mode.Override(DepthOfFieldMode.Bokeh);

                dof.focusDistance.overrideState = true;

                dof.focusDistance.value = focusDistance;
            }
            else
            {
                globalVolume.priority = 0;

                globalVolume.weight = 0f;

                dof.mode.Override(DepthOfFieldMode.Gaussian);
            }
        }  
    }
}
