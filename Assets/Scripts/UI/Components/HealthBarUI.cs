using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Implements the HealthBar UI element.
/// </summary>
public class HealthBarUI : MonoBehaviour
{
    private Image healthBarImage;

    void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        healthBarImage = transform.Find("HealthBG/ProgressBar").GetComponent<Image>();
    }

    /// <summary>
    /// Sets the progress bar to reflect the provided value.
    /// </summary>
    /// <param name="health">Value from 0 to 100 that should be shown on the progress bar.</param>
    public void SetHealth(float health)
    {
        healthBarImage.fillAmount = health / 100;
    }
}
