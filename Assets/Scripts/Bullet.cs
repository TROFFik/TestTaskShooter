using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Action<Bullet> collisionAction;

    [SerializeField] private float _speed = 50.0f;

    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        collisionAction?.Invoke(this);
    }
}
