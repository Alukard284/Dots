using DefaultNamespace;
using DefaultNamespace.Components.Interfaces;
using System;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    public GameObject bullet;
    public float shootDelay;

    private Vector3 _firePoint = new Vector3(0, 1.6f, 0.4f);
    private float _shootTime = float.MinValue;
    public PlayerStats stats;

    private void Start()
    {
        var jsonString = PlayerPrefs.GetString("Stats");
        if (!jsonString.Equals(string.Empty, StringComparison.Ordinal))
        {
            stats = JsonUtility.FromJson<PlayerStats>(jsonString);
        }
        else 
        {
            stats = new PlayerStats();
        }
    }
    public void Execute()
   {
        if(Time.time < _shootTime + shootDelay) return;

        _shootTime = Time.time;

        if (bullet != null)
        {
            var t = transform;
            var newBullet = Instantiate(bullet, t.position + _firePoint, t.rotation);
            stats.shootCount++;
        }
        else { Debug.Log("[SHOOT ABILITY] No bullet prefab link!"); }
   }
}
