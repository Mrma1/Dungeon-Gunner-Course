using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	[SerializeField] private MovementDetailsSO movementDetails;
    [SerializeField] private Transform weaponShootPosition;

    private Player player;
	private float moveSpeed;

	private void Awake()
	{
		player = GetComponent<Player>();
		moveSpeed = movementDetails.GetMoveSpeed();
	}

	private void Update()
	{
		MovementInput();

		WeaponInput();
	}

	private void MovementInput()
	{
		float horizontalMovement = Input.GetAxisRaw("Horizontal");
		float verticalMovement = Input.GetAxisRaw("Vertical");

		Vector2 direction = new Vector2(horizontalMovement, verticalMovement);

		if (direction.x != 0 && direction.y != 0) 
		{
			direction *= 0.7f;
		}

		if(direction != Vector2.zero)
		{
			player.movementByVelocityEvnet.CallMovementByVelocityEvent(direction, moveSpeed);
		}
		else
		{
			player.idleEvent.CallIdleEvent();
		}	
	}

	private void WeaponInput()
	{
		Vector3 weaponDirection;
		float weaponAngleDegrees, playerAngleDegrees;
		AimDirection playerAimDirection;

		AimWeaponInput(out weaponDirection, out weaponAngleDegrees, out playerAngleDegrees, out playerAimDirection);
	}

	private void AimWeaponInput(out Vector3 weaponDirection, out float weaponAngleDegrees, out float playerAngleDegrees, out AimDirection playerAimDirection)
	{
		Vector3 mouseWorldPosition = HelperUtilities.GetMouseWorldPosition();
		weaponDirection = mouseWorldPosition - weaponShootPosition.position;

		Vector3 playerDirection = mouseWorldPosition - transform.position;

		weaponAngleDegrees = HelperUtilities.GetAngleFromVector(weaponDirection);
		playerAngleDegrees = HelperUtilities.GetAngleFromVector(playerDirection);
		playerAimDirection = HelperUtilities.GetAimDirection(playerAngleDegrees);

		player.aimWeaponEvent.CallAimWeaponEvent(playerAimDirection, playerAngleDegrees, weaponAngleDegrees, weaponDirection);
	}

	#region Validation

#if UNITY_EDITOR

	private void OnValidate()
	{
		HelperUtilities.ValidateCheckNullValue(this, nameof(movementDetails), movementDetails);
	}

#endif

	#endregion
}
