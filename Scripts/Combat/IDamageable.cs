using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{

    public IEnumerator Combat(int damage, float criticalRate, int manyHits);   
}
