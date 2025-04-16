using System;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponType
{
    List<GameObject> Bullets { get; }
    void CanFire(Action onCanFire);
    void Fire();
}
