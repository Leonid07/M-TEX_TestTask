using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ScriptableInstaller", menuName = "Installers/ScriptableInstaller")]
public class ScriptableInstaller : ScriptableObjectInstaller<ScriptableInstaller>
{
    public BulletData bulletData;
    public override void InstallBindings()
    {
        Container.BindInstance(bulletData);
    }
}