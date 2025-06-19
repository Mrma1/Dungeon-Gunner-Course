using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Health))]
[RequireComponent (typeof(PlayerControl))]
[RequireComponent (typeof(IdleEvent))]
[RequireComponent(typeof(Idle))]
[RequireComponent(typeof(AimWeaponEvent))]
[RequireComponent(typeof(AimWeapon))]
[RequireComponent (typeof(MovementByVelocity))]
[RequireComponent(typeof(MovementByVelocityEvnet))]
[RequireComponent(typeof(MovementToPosition))]
[RequireComponent(typeof(MovementToPositionEvent))]
[RequireComponent (typeof(AnimatePlayer))]
[RequireComponent(typeof(SortingGroup))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerDetailsSO playerDetails;
    [HideInInspector] public Health health;
	[HideInInspector] public IdleEvent idleEvent;
	[HideInInspector] public AimWeaponEvent aimWeaponEvent;
	[HideInInspector] public MovementByVelocityEvnet movementByVelocityEvnet;
	[HideInInspector] public MovementToPositionEvent movementToPositionEvent;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public Animator animator;

	private void Awake()
	{
		health = GetComponent<Health>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		idleEvent = GetComponent<IdleEvent>();
		aimWeaponEvent = GetComponent<AimWeaponEvent>();
		movementByVelocityEvnet = GetComponent<MovementByVelocityEvnet>();
		movementToPositionEvent = GetComponent<MovementToPositionEvent>();
	}

	public void Initialize(PlayerDetailsSO playerDetails)
	{
		this.playerDetails = playerDetails;

		SetPlayerHealth();
	}

	private void SetPlayerHealth()
	{
		health.SetStartingHealth(playerDetails.playerHealthAmount);
	}
}
