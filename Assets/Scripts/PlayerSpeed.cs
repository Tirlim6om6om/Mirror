using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    public NetworkIdentity identity;
    private Vector3 _speed;
    private Vector3 _oldPos;

    private void Start()
    {
        _oldPos = transform.position;
    }

    private void FixedUpdate()
    {
        _speed = transform.position - _oldPos;
        _oldPos = transform.position;
    }

    public Vector3 GetSpeed() => _speed;
}
