using UnityEngine;

//Interface that get the damage amount, critical rate and how many hits will cause

public interface IDamageable
{
    public IEnumerator Combat(int damage, float criticalRate, int manyHits);   
}
