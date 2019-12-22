
public class ElfSpawnManager : SpawnManager<Elf>
{
    public override Elf Spawn()
    {
        Elf elf = base.Spawn();
        elf.ResetLife();
        return elf;
    }
}