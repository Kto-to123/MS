using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    /// <summary>
    /// Получить урон с бронипробитием
    /// </summary>
    /// <param name="_Damage"></param>
    /// <param name="armorPenetration"></param>
    void GetDamage(double _Damage, int armorPenetration);

    /// <summary>
    /// Получить урон, бронепробитие = 0
    /// </summary>
    /// <param name="_Damage"></param>
    void GetDamage(double _Damage);
}
