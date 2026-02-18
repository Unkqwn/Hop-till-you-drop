using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    protected float Health { get; }

    public void TakeDamage(float damage);
}
