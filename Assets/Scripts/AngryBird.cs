using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBird : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Collider2D _collider;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _rb.isKinematic = true;
        _collider.enabled = false;
    }

    public void Launch(Vector2 direction, float force)
    {
        _rb.isKinematic=false;
        _collider.enabled = true;
        _rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
}
