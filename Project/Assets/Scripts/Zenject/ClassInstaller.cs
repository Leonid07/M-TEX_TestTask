using Zenject;

public class ClassInstaller : MonoInstaller
{
    // ����� InstallBindings ���������� ��� ��������� �������� � ���������� Zenject.
    public override void InstallBindings()
    {
        // �������� ������� � �� ����������� � ���������� Zenject.
        Container.Bind<Menu>().FromInstance(FindObjectOfType<Menu>()).AsTransient(); // �������� ������ Menu
        Container.Bind<PlayerShooting>().FromInstance(FindObjectOfType<PlayerShooting>()).AsSingle(); // �������� ������ PlayerShooting
        Container.Bind<PlayerMoving>().FromInstance(FindObjectOfType<PlayerMoving>()).AsSingle(); // �������� ������ PlayerMoving
        Container.Bind<Player>().FromInstance(FindObjectOfType<Player>()).AsSingle(); // �������� ������ Player

        Container.Bind<DropDownBonus>().FromComponentSibling().AsTransient(); // �������� ������ DropDownBonus
    }
}