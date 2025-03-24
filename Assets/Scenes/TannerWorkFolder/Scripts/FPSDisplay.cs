using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FPSDisplay : MonoBehaviour
{
    private float avgFrameRate;
    public TextMeshProUGUI display_Text;

    public void Update()
    {
        avgFrameRate += (Time.deltaTime - avgFrameRate) * 0.1f;
        float current = 1.0f / avgFrameRate;
        display_Text.text = Mathf.Ceil(current).ToString() + " FPS";
    }
}