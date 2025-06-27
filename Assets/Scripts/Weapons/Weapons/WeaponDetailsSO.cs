using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDetails_", menuName = "Scriptable Objects/Weapons/Weapon Details")]
public class WeaponDetailsSO : ScriptableObject
{
    public string weaponName;
    public Sprite weaponSprite;
    public Vector3 weaponShootPosition;
    public AmmoDetailsSO weaponCurrentAmmo;

    [Header("无限弹药")]
    public bool hasInfiniteAmmo = false;
    [Header("无限弹药容量")]
    public bool hasInfiniteClipCapacity = false;
    [Header("一次弹夹容量")]
    public int weaponClipAmmoCapacity = 6;
    [Header("总弹药容量")]
    public int weaponAmmoCapacity = 100;
    [Header("武器射速")]
    public float weaponFireRate = 0.2f;
    [Header("射击间隔")]
    public float weaponPrechargeTime = 0f;
	[Header("弹药填充速度")]
	public float weaponReloadTime = 0f;

	#region  Validation

#if UNITY_EDITOR
	private void OnValidate()
	{
		HelperUtilities.ValidateCheckEmptyString(this, nameof(weaponName), weaponName);
        HelperUtilities.ValidateCheckNullValue(this, nameof(weaponCurrentAmmo), weaponCurrentAmmo);
        HelperUtilities.ValidateCheckPositiveValue(weaponSprite, nameof(weaponFireRate), weaponFireRate, false);
		HelperUtilities.ValidateCheckPositiveValue(weaponSprite, nameof(weaponPrechargeTime), weaponFireRate, true);


		if (!hasInfiniteAmmo)
        {
			HelperUtilities.ValidateCheckPositiveValue(weaponSprite, nameof(weaponAmmoCapacity), weaponAmmoCapacity, false);
		}

        if(!hasInfiniteClipCapacity)
        {
			HelperUtilities.ValidateCheckPositiveValue(weaponSprite, nameof(weaponClipAmmoCapacity), weaponClipAmmoCapacity, false);
		}
	}
#endif

	#endregion
}
