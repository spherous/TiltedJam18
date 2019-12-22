

public class EnemySpawnManager : SpawnManager<Enemy>
{
    public override Enemy Spawn()
    {
        Enemy enemy = base.Spawn();
        enemy.ResetLife();
        return enemy;
    }
}