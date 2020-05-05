using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Image healthBarImage;
    void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;

        healthBarImage = transform.Find("HealthBG/ProgressBar").GetComponent<Image>();
    }

    public void SetHealth(float health)
    {
        healthBarImage.fillAmount = health / 100;
    }
}
