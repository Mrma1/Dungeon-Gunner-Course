using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MovementByVelocityEvnet : MonoBehaviour
{
	public event Action<MovementByVelocityEvnet, MovementByVelocityArgs> OnMovementByVelocity;

	public void CallMovementByVelocityEvent(Vector2 moveDirection, float moveSpeed)
	{
		OnMovementByVelocity?.Invoke(this, new MovementByVelocityArgs() { mouveDiection = moveDirection, moveSpeed = moveSpeed});
	}
}

public class MovementByVelocityArgs : EventArgs
{
	public Vector2 mouveDiection;
	public float moveSpeed;
}