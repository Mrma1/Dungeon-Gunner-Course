using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Ammo : MonoBehaviour, IFireable
{
	[SerializeField] private TrailRenderer trailRenderer;

	// ��̷�Χ
	private float ammoRange = 0f;
	private float ammoSpeed;
	private Vector3 fireDirectionVector;
	private float fireDirectionAngle;
	private SpriteRenderer spriteRenderer;
	private AmmoDetailsSO ammoDetails;
	//����ʱ
	private float ammoChargeTimer;
	private bool isAmmoMaterialSet = false;
	private bool overrideAmmoMovement;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		if(ammoChargeTimer > 0f)
		{
			ammoChargeTimer -= Time.deltaTime;
			return;
		}
		else if(!isAmmoMaterialSet)
		{
			SetAmmoMaterial(ammoDetails.ammoMaterial);
			isAmmoMaterialSet = true;
		}

		//�ƶ�
		Vector3 distanceVector = fireDirectionVector * ammoSpeed * Time.deltaTime;
		transform.position += distanceVector;

		ammoRange -= distanceVector.magnitude;

		if(ammoRange <0f)
		{
			DisableAmmo();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		DisableAmmo();
	}

	public void InitialiseAmmo(AmmoDetailsSO ammoDetails, float aimAngle, float weaponAimAngle, float ammoSpeed, Vector3 weaponAimDirectionVector, bool overrideAmmoMovement = false)
	{
		#region Ammo
		this.ammoDetails = ammoDetails;

		SetFirDirection(ammoDetails, aimAngle, weaponAimAngle, weaponAimDirectionVector);

		spriteRenderer.sprite = ammoDetails.ammoSprite;

		if(ammoDetails.ammoChargeTime > 0f)
		{
			ammoChargeTimer = ammoDetails.ammoChargeTime;
			SetAmmoMaterial(ammoDetails.ammoChargeMaterial);
			isAmmoMaterialSet = false;
		}
		else
		{
			ammoChargeTimer = 0f;
			SetAmmoMaterial(ammoDetails.ammoMaterial);
			isAmmoMaterialSet = true;
		}

		ammoRange = ammoDetails.ammoRange;
		this.ammoSpeed = ammoSpeed;
		this.overrideAmmoMovement = overrideAmmoMovement;

		gameObject.SetActive(true);

		#endregion

		#region  Trail

		if (ammoDetails.isAmmoTrail)
		{
			trailRenderer.gameObject.SetActive(true);
			trailRenderer.emitting = true;
			trailRenderer.material = ammoDetails.ammoTrailMaterial;
			trailRenderer.startWidth = ammoDetails.ammoTrailStartWidth;
			trailRenderer.endWidth = ammoDetails.ammoTrailEndWidth;
			trailRenderer.time = ammoDetails.ammoTrailTime;
		}
		else
		{
			trailRenderer.emitting = false;
			trailRenderer.gameObject.SetActive(false);
		}

		#endregion
	}

	private void SetFirDirection(AmmoDetailsSO ammoDetails, float aimAngle, float weaponAimAngle, Vector3 weaponAimDirectionVector)
	{
		//���ɢ���С
		float randomSpread = Random.Range(ammoDetails.ammoSpreadMin, ammoDetails.ammoSpreadMax);

		//��� -1����1
		int spreadToggle = Random.Range(0, 2) * 2 - 1;

		//�ж�����������룬���С����С����ʹ������Ƕȣ�����ʹ�������Ƕ�
		if(weaponAimDirectionVector.magnitude < Settings.useAimAngleDistance)
		{
			fireDirectionAngle = aimAngle;
		}
		else
		{
			fireDirectionAngle = weaponAimAngle;
		}

		//���ɢ��ƫ��
		fireDirectionAngle += spreadToggle * randomSpread;
		//Ӧ�ýǶ�
		transform.transform.eulerAngles = new Vector3(0, 0, fireDirectionAngle);

		fireDirectionVector = HelperUtilities.GetDirectionVectorFromAngle(fireDirectionAngle);
	}

	private void DisableAmmo()
	{
		gameObject.SetActive(false);
	}

	public void SetAmmoMaterial(Material material)
	{
		spriteRenderer.material = material;
	}

	public GameObject GetGameObject()
	{
		return gameObject;
	}

	#region  Validation
#if UNITY_EDITOR

	private void OnValidate()
	{
		HelperUtilities.ValidateCheckNullValue(this, nameof(trailRenderer), trailRenderer);
	}

#endif
	#endregion
}
