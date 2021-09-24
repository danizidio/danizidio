public interface InterfacePlayerCombat
{
    void EnemyHitPlayer(int enmyDmg, int playerDef, int playerLife);

}

public interface InterfaceEnemyCombat
{
    void PlayerHitEnemy(int plyrDmg, int enemyDef, float criticalRate);
}

public interface IShowDamage
{
    void ShowText(bool isCritical, float damage, UnityEngine.GameObject spawnPoint);
}