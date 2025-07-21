using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementByVelocityEvnet))]
[DisallowMultipleComponent]
public class MovementByVelocity : MonoBehaviour
{
    private Rigidbody2D rg2D;
    private MovementByVelocityEvnet movementByVelocityEvnet;

    private void Awake()
    {
        rg2D = GetComponent<Rigidbody2D>();
        movementByVelocityEvnet = GetComponent<MovementByVelocityEvnet>();
    }

    private void OnEnable()
    {
        movementByVelocityEvnet.OnMovementByVelocity += MovementByVelocityEvnet_OnMovementByVelocity;
    }

    private void OnDisable()
    {
        movementByVelocityEvnet.OnMovementByVelocity -= MovementByVelocityEvnet_OnMovementByVelocity;
    }

    private void MovementByVelocityEvnet_OnMovementByVelocity(MovementByVelocityEvnet movementByVelocityEvnet, MovementByVelocityArgs movementByVelocityArgs)
    {
        MoveRigidBody(movementByVelocityArgs.mouveDiection, movementByVelocityArgs.moveSpeed);
    }

    private void MoveRigidBody(Vector2 moveDiection, float moveSpeed)
    {
        rg2D.velocity = moveDiection * moveSpeed;
    }
}