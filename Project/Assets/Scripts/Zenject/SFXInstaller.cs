using UnityEngine;
using Zenject;

public class SFXInstaller : MonoInstaller
{
    public AudioClip soundWeapon;

    public override void InstallBindings()
    {
        Container.Bind<AudioClip>().WithId("SoundWeapon").FromInstance(soundWeapon).AsTransient();
    }
}
