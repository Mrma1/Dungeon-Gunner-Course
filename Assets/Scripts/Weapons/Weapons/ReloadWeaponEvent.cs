using System;
using UnityEngine;

[DisallowMultipleComponent]
public class ReloadWeaponEvent : MonoBehaviour
{
    public event Action<ReloadWeaponEvent, ReloadWeaponEventArgs> OnReloadWeapon;

    public void CallReloadWeaponEvent(Weapon weapon, int topUpAmmoPercent)
    {
        OnReloadWeapon?.Invoke(this, new ReloadWeaponEventArgs() { weapon = weapon, topUpAmmoPercent = topUpAmmoPercent });
    }
}

public class ReloadWeaponEventArgs : EventArgs
{
    public Weapon weapon;

    /// <summary>
    /// 填充的最大百分比
    /// </summary>
    public int topUpAmmoPercent;
}