using System;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class PlayerShooting : MonoBehaviour, ISFX
{
    [Inject(Id = "BulletPrefabPlayer")] GameObject bulletPrefab;// префаб пули игрока
    [Inject(Id = "ShotWeaponEffect")] ParticleSystem weaponParticleSystem;// генератор частиц для выстрелов оружия
    [Inject (Id = "SoundWeapon")] AudioClip weaponSound;
    [Inject] DiContainer container;

    [SerializeField][Range(0, 1)] float volume = 0.5f;
    [SerializeField] public int levelWeapons = 0;// уровень оружия игрока
    [SerializeField] float fireRate = 1f;// скорость выстрелов

    public Transform[] weaponPositions;// позиции оружия игрока

    void Start()
    {
        Observable.Interval(TimeSpan.FromSeconds(fireRate))
            .Subscribe(_ =>
            {
                switch (levelWeapons)
                {
                    case 0:
                        ActivateWeapons(0);
                        break;
                    case 1:
                        ActivateWeapons(1);
                        break;
                    case 2:
                        ActivateWeapons(2);
                        break;
                    default:
                        ActivateWeapons(2);
                        break;
                }
            })
            .AddTo(this);
    }

    void ActivateWeapons(int numberOfWeapons)
    {
        PlayingSoundWeapon(weaponSound, Camera.main.transform.position, volume);
        if (numberOfWeapons == 0)
        {
            InstantiateBulletAtPosition(weaponPositions[0]);
        }
        else if (numberOfWeapons == 1)
        {
            InstantiateBulletAtPosition(weaponPositions[1]);
            InstantiateBulletAtPosition(weaponPositions[2]);
        }
        else if (numberOfWeapons == 2)
        {
            foreach (Transform weaponPosition in weaponPositions)
            {
                InstantiateBulletAtPosition(weaponPosition);
            }
        }
    }

    void InstantiateBulletAtPosition(Transform weaponPosition)
    {
        container.InstantiatePrefabForComponent<IBullet>(bulletPrefab, weaponPosition.position, Quaternion.identity, null);
        Instantiate(weaponParticleSystem, weaponPosition.position, Quaternion.identity, transform);
    }

    public void PlayingSoundWeapon(AudioClip sound, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(sound, position, volume);
    }
}