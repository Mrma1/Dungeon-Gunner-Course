using UnityEngine;

public class Weapon
{
    public WeaponDetailsSO weaponDetails;
    /// <summary>
    /// 武器列表位置
    /// </summary>
    public int weaponListPosition;
    /// <summary>
    /// 武器装填计时器
    /// </summary>
    public float weaponReloadTimer;
    /// <summary>
    /// 武器弹夹剩余子弹数
    /// </summary>
    public int weaponClipRemainingAmmo;
    /// <summary>
    /// 武器总剩余子弹数
    /// </summary>
    public int weaponRemainingAmmo;
    /// <summary>
    /// 是否正在装填
    /// </summary>
    public bool isWeaponReloading;
}
