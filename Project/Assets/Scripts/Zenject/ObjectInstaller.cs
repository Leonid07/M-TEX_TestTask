using UnityEngine;
using Zenject;

public class ObjectInstaller : MonoInstaller
{
    // Объявление публичных переменных для префабов игрока
    public GameObject playerPrefab;// Префаб игрока
    public GameObject bulletPrefabPlayer;// Префаб пули игрока

    // Объявление публичных переменных для префабов врага
    public GameObject enemyPrefab;// Префаб врага
    public GameObject bulletPrefab;// Префаб пули врага

    // Метод InstallBindings вызывается для установки привязок в контейнере Zenject.
    public override void InstallBindings()
    {
        // Привязка префабов к их идентификаторам в контейнере Zenject.
        Container.Bind<GameObject>().WithId("PlayerPrefab").FromInstance(playerPrefab).AsTransient();
        Container.Bind<GameObject>().WithId("BulletPrefabPlayer").FromInstance(bulletPrefabPlayer).AsTransient();
        Container.Bind<GameObject>().WithId("BulletPrefab").FromInstance(bulletPrefab).AsTransient();
        Container.Bind<GameObject>().WithId("EnemyPrefab").FromInstance(enemyPrefab).AsTransient();

        // Привязка интерфейсов к компонентам в новых префабах.
        Container.Bind<IEnemy>().FromComponentInNewPrefab(enemyPrefab).AsTransient();
        Container.Bind<IBullet>().FromComponentInNewPrefab(bulletPrefab).AsTransient();
        Container.Bind<IBullet>().FromComponentInNewPrefab(bulletPrefabPlayer).AsTransient();
        Container.Bind<ISFX>().FromComponentInNewPrefab(playerPrefab).AsTransient();
    }
}