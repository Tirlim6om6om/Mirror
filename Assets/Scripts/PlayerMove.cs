using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerMove : NetworkBehaviour
{
    public Vector3 velocity;
    [SerializeField] private float moveSpeedMultiplier = 1;
    private bool local;
    private Rigidbody _rb;
    public LagCompensationSettings lagCompensationSettings = new LagCompensationSettings();
    
    public override void OnStartAuthority()
    {
        local = true;
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        TryGetComponent(out _rb);
        this.enabled = true;
        TryGetComponent(out NetworkTransformReliable component);
        if (isServer)
        {
            component.syncDirection = SyncDirection.ServerToClient;
            component.syncMode = SyncMode.Observers;
        }
        else
        {
            component.syncDirection = SyncDirection.ClientToServer;
        }
    }

    public override void OnStopAuthority()
    {
        this.enabled = false;
        local = false;
    }

    private void FixedUpdate()
    {
        if(!local)
            return;
        
        // Capture inputs
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Create initial direction vector without jumpSpeed (y-axis).
        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        // Clamp so diagonal strafing isn't a speed advantage.
        direction = Vector3.ClampMagnitude(direction, 1f);

        // Transforms direction from local space to world space.
        direction = transform.TransformDirection(direction);

        // Multiply for desired ground speed.
        direction *= moveSpeedMultiplier;

        direction = new Vector3(direction.x, _rb.velocity.y, direction.z);
        
        // Finally move the character.
        _rb.velocity = direction * Time.deltaTime;
        velocity = direction * Time.deltaTime;
    }
}
