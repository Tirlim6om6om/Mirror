using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;

    private void Start()
    {
        TryGetComponent(out _rb);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMove playerMove) &&
            collision.gameObject.TryGetComponent(out Rigidbody rb))
        {
            _rb.AddForce(new Vector3(0, 200, 0));
        }
    }
}
