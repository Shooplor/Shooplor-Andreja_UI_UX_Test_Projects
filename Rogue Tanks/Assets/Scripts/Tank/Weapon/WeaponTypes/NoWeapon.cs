using System;
using System.Collections.Generic;
using UnityEngine;

public class NoWeapon : IWeaponType
{
    public List<GameObject> Bullets { get; private set; } = new List<GameObject>();
    public void CanFire(Action onCanFire) { }

    public void Fire() { }
}
