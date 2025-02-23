﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public float CurrentHealth { get; }
    public float MaxHealth { get; }
    public void TakeDamage(float amount, Vector3 hitPoint);

    public bool IsHit { get; } 
}
