using DefaultNamespace.Components.Interfaces;
using UnityEngine;

public class JerkAbility : MonoBehaviour, IAbility
{
    public Rigidbody rb;

    [Header("Jerk Settings")]
    public float jerkForce = 10f;     // ���� �����
    public float jerkCooldown = 2f;  // ����� ����������� �����
    private float _lastJerkTime = -Mathf.Infinity; // ����� ���������� �����

    public void Execute()
    {
        if(Time.time < _lastJerkTime + jerkCooldown) return;
        // ��������� ����� ���������� �����
        _lastJerkTime = Time.time;

        if (rb != null)
        {
            Vector3 jerkDirection = rb.transform.forward; // ����������� �����
            rb.AddForce(jerkDirection * jerkForce, ForceMode.Impulse);

            Debug.Log($"[JerkAbility] Jerk executed with force {jerkForce}.");
        }
    }
}
