using Zenject;

public class ClassInstaller : MonoInstaller
{
    // Метод InstallBindings вызывается для установки привязок в контейнере Zenject.
    public override void InstallBindings()
    {
        // Привязка классов к их экземплярам в контейнере Zenject.
        Container.Bind<Menu>().FromInstance(FindObjectOfType<Menu>()).AsTransient(); // Привязка класса Menu
        Container.Bind<PlayerShooting>().FromInstance(FindObjectOfType<PlayerShooting>()).AsSingle(); // Привязка класса PlayerShooting
        Container.Bind<PlayerMoving>().FromInstance(FindObjectOfType<PlayerMoving>()).AsSingle(); // Привязка класса PlayerMoving
        Container.Bind<Player>().FromInstance(FindObjectOfType<Player>()).AsSingle(); // Привязка класса Player

        Container.Bind<DropDownBonus>().FromComponentSibling().AsTransient(); // Привязка класса DropDownBonus
    }
}