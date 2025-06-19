using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDetails_", menuName = "Scriptable Objects/Weapons/Weapon Details")]
public class WeaponDetailsSO : ScriptableObject
{
    public string weaponName;
    public Sprite weaponSprite;
    public Vector3 weaponShootPosition;
    public AmmoDetailsSO weaponCurrentAmmo;

    [Header("���޵�ҩ")]
    public bool hasInfiniteAmmo = false;
    [Header("���޵�ҩ����")]
    public bool hasInfiniteClipCapacity = false;
    [Header("һ�ε�������")]
    public int weaponClipAmmoCapacity = 6;
    [Header("�ܵ�ҩ����")]
    public int weaponAmmoCapacity = 100;
    [Header("��������")]
    public float weaponFireRate = 0.2f;
    [Header("������")]
    public float weaponPrechargeTime = 0f;
	[Header("��ҩ����ٶ�")]
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
