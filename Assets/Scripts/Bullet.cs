using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody _bulletRb;
    private float _speed = 100f;
    private float _timeToLive = 10f;
    
    void Start()
    {
        _bulletRb = GetComponent<Rigidbody>();
        Destroy(gameObject, _timeToLive);
    }

    void Update()
    {
        _bulletRb.velocity = transform.forward * _speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
