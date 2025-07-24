using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponShootEffect_", menuName = "Scriptable Objects/Weapons/Weapon Shoot Effect")]
public class WeaponShootEffectSO : ScriptableObject
{
    [Header("颜色渐变选择器")] public Gradient colorGradient;

    [Header("持续时间")] public float duration = 0.50f;
    [Header("起始大小")] public float startParticleSize = 0.25f;
    [Header("起始速度")] public float startParticleSpeed = 3f;
    [Header("粒子寿命")] public float startLifetime = 0.5f;
    [Header("粒子最大数量")] public int maxParticleNumber = 100;

    [Header("发射率，每秒发射多少粒子")] public int emissionRate = 100;
    [Header("每秒发射多少粒子")] public int burstParticleNumber = 20;
    [Header("重力，负数向上飘")] public float effectGravity = -0.01f;
    public Sprite sprite;

    public Vector3 velocityOverLifetimeMin;
    public Vector3 velocityOverLifetimeMax;
    public GameObject weaponShootEffectPrefab;

    #region Validation

#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(duration), duration, false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(startParticleSize), startParticleSize, false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(startParticleSpeed), startParticleSpeed, false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(startLifetime), startLifetime, false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(maxParticleNumber), maxParticleNumber, false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(emissionRate), emissionRate, true);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(burstParticleNumber), burstParticleNumber, false);
        HelperUtilities.ValidateCheckNullValue(this, nameof(weaponShootEffectPrefab), weaponShootEffectPrefab);
    }
#endif

    #endregion
}