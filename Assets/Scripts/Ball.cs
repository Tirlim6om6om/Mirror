using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;
    private PredictedRigidbody _predictedRigidbody;
    private NetworkIdentity _identity;

    private void Start()
    {
        TryGetComponent(out _rb);
        TryGetComponent(out _predictedRigidbody);
        TryGetComponent(out _identity);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerSpeed player))
        {
            _rb.AddForce(player.GetSpeed() * 1000 + new Vector3(0, 1000 * player.GetSpeed().magnitude, 0));
        }
    }
}
