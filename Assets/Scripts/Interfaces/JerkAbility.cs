using DefaultNamespace.Components.Interfaces;
using UnityEngine;

public class JerkAbility : MonoBehaviour, IAbility
{
    public Rigidbody rb;

    [Header("Jerk Settings")]
    public float jerkForce = 10f;     // Сила рывка
    public float jerkCooldown = 2f;  // Время перезарядки рывка
    private float _lastJerkTime = -Mathf.Infinity; // Время последнего рывка

    public void Execute()
    {
        if(Time.time < _lastJerkTime + jerkCooldown) return;
        // Обновляем время последнего рывка
        _lastJerkTime = Time.time;

        if (rb != null)
        {
            Vector3 jerkDirection = rb.transform.forward; // Направление рывка
            rb.AddForce(jerkDirection * jerkForce, ForceMode.Impulse);

            Debug.Log($"[JerkAbility] Jerk executed with force {jerkForce}.");
        }
    }
}
