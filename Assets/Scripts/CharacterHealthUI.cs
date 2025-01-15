using UnityEngine;
using UnityEngine.UI;
using DefaultNamespace;

public class CharacterHealthUI : MonoBehaviour
{
    [SerializeField] private Image HealthBar;
    [SerializeField] private CharacterHealth characterHealth;
    private int maxHealth = 100;
    private float lerpSpeed;

    void Update()
    {
        lerpSpeed = 5 * Time.deltaTime;
        if (characterHealth.Health > maxHealth) { characterHealth.Health = maxHealth; }
        HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, (float)characterHealth.Health / maxHealth, lerpSpeed);
    }
}
