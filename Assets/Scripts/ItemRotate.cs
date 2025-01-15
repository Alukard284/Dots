using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    private Transform _itemTransform;
    [SerializeField] private float _rotateSpeed = 50f;

    void Start()
    {
        _itemTransform = GetComponent<Transform>();
    }

    void Update()
    {
        _itemTransform.Rotate(Vector3.forward, _rotateSpeed * Time.deltaTime);
    }
}
