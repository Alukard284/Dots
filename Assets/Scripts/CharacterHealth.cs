using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    public int Health;
    private int _maxHealth = 100;
    private float lerpSpeed;
    [SerializeField] private Image HealthBar;

    private void Start()
    {
        Health = _maxHealth;
    }
    private void Update()
    {
        lerpSpeed = 5* Time.deltaTime;
        if (Health > _maxHealth) { Health = _maxHealth; }
        HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount,(float)Health / _maxHealth, lerpSpeed);
    }
}
