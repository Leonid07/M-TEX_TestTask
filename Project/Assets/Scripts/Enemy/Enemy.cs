using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class Enemy : MonoBehaviour, IEnemy
{
    [Inject] DropDownBonus dropDownBonus;
    [Inject] Menu menu;

    [Inject(Id = "BulletPrefab")] GameObject bulletPrefab;// префаб снаряда выпускаемого противником
    [Inject(Id = "HitEffect")] ParticleSystem hitEffect;// генератор частиц для регистрации попадания
    [Inject(Id = "DestroyEffect")] ParticleSystem destroyEffect;// генератор частиц уничтожения объекта

    [Inject] DiContainer container;

    [SerializeField] float health = 10f;// количество здоровья
    [SerializeField] float speed = 10f; // Скорость противника
    [SerializeField] float rotationSpeed = 50f; // Скорость вращения
    [SerializeField] int numberPointsPerKill = 100;// количество получаемых очков после убийства объекта
    [SerializeField] int minFireRate = 10;// минимальная задержка между выстрелами
    [SerializeField] int maxFireRate = 20;// максимальная задержка между выстрелами

    Vector3 targetPosition; // место на которое объект долже встать
    float fireRate; // Скорость стрельбы
    float timeSinceLastShot = 0f; // Время с последнего выстрела

    public void SetTargetPosition(Vector3 target)
    {
        targetPosition = target;
    }

    void Start()
    {
        fireRate = Random.Range(minFireRate, maxFireRate);

        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                if (health <= 0)
                {
                    Death();
                }
                else
                {
                    // Двигаем противника к цели
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                    // Добавляем вращение по оси Z
                    transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

                    if (transform.position == targetPosition)
                    {
                        // Если пришло время стрелять, создаем пулю
                        timeSinceLastShot += Time.deltaTime;
                        if (timeSinceLastShot > fireRate)
                        {
                            Fire();
                            timeSinceLastShot = 0f; // Сброс времени с последнего выстрела
                        }
                    }
                }
            })
            .AddTo(this);

        this.OnTriggerEnterAsObservable()
            .Where(other => other.gameObject.tag == "Player")
            .Subscribe(other => Destroy(other.gameObject))
            .AddTo(this);
    }

    void Death()
    {
        dropDownBonus.SpawnBonus();
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        menu.AccountChange(numberPointsPerKill);
        Destroy(gameObject);
    }

    void Fire()
    {
        container.InstantiatePrefabForComponent<IBullet>(bulletPrefab, transform.position, Quaternion.identity, null);
    }

    public void GetHealth(float health)
    {
        this.health -= health;
        Instantiate(hitEffect, transform.position, Quaternion.identity);
    }
}
