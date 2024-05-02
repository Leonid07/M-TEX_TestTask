using UnityEngine;
using Zenject;

public class ParticleInstaller : MonoInstaller
{
    // Системы частиц, связанные с игроком
    public ParticleSystem shotWeaponEffect;// Эффект выстрела оружия игрока
    public ParticleSystem destroyPlayer;// Эффект уничтожения игрока

    // Системы частиц, связанные с врагом
    public ParticleSystem hitEffect;// Эффект попадания в врага
    public ParticleSystem destroyEffect;// Эффект уничтожения врага
    public override void InstallBindings()
    {
        // Привязка систем частиц к их идентификаторам в контейнере Zenject.
        Container.Bind<ParticleSystem>().WithId("HitEffect").FromInstance(hitEffect).AsTransient();
        Container.Bind<ParticleSystem>().WithId("DestroyEffect").FromInstance(hitEffect).AsTransient();

        Container.Bind<ParticleSystem>().WithId("ShotWeaponEffect").FromInstance(shotWeaponEffect).AsTransient();
        Container.Bind<ParticleSystem>().WithId("DestroyPlayer").FromInstance(destroyPlayer).AsTransient();
    }
}