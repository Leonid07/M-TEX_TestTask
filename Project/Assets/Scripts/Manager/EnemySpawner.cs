using System;
using System.Collections;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;
public class EnemySpawner : MonoBehaviour
{
    [Inject(Id = "EnemyPrefab")] GameObject enemyPrefab;// префаб для противника который будет создан
    [Inject(Id = "TextRoundCounter")] TMP_Text textRoundCounter;// текст для отображения количества волн
    [SerializeField] float spawnRate = 1f;// задержка между волнами противников
    [SerializeField] float spawnDistance = 10f;// задержка перед первый созданием волны противников

    [SerializeField] int rows = 3;// количесвто сток противников
    [SerializeField] int columns = 11;// количество столбцов противников
    [SerializeField] float spacing = 1.0f;// расстояния между противниками

    [Tooltip("Координаты стартовой точки от которой будут создаваться противники")]
    [SerializeField] Vector2 startPosition = new Vector2(-5, 2);

    int leftOrRightSwitch = 0;// переключатель спана 0-спавн будет с левой стороны экрана / 1-спавн с правой стороны экрана
    bool isSpawning = false;    // одна из проверок для того чтобы волны не спавнились одна за одной
    int roundCount;// счётчик раундов

    [Inject] DiContainer container;
    IDisposable updateSubscription;

    void Start()
    {
        updateSubscription = Observable.EveryUpdate()
            // две проверки на то чтобы волны не спавнились одновременно transform.childCount проверка на количество дитей у родителя
            .Where(_ => transform.childCount == 0 && !isSpawning)
            .Subscribe(_ => StartDelay());
    }

    void StartDelay()
    {
        isSpawning = true;
        Observable.ReturnUnit()
            .Delay(TimeSpan.FromSeconds(2))
            .Do(_ =>
            {
                roundCount++;
                textRoundCounter.text = $"Раунд {roundCount}";
            })
            .Subscribe(_ =>
            {
                StartCoroutine(SpawnEnemies());
                isSpawning = false;
            });
    }

    IEnumerator SpawnEnemies()
    {
        isSpawning = true;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                leftOrRightSwitch = UnityEngine.Random.Range(0, 2);
                Vector3 position = new Vector3(startPosition.x + j * spacing, startPosition.y + i * spacing, 0);
                if (leftOrRightSwitch == 0)
                {
                    Vector3 leftSpawnPoint = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0) + Camera.main.transform.right * -spawnDistance;
                    SpawnEnemyAtPosition(leftSpawnPoint, position);
                }
                else
                {
                    Vector3 rightSpawnPoint = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0) + Camera.main.transform.right * spawnDistance;
                    SpawnEnemyAtPosition(rightSpawnPoint, position);
                }
                yield return new WaitForSeconds(spawnRate);
            }
        }
        isSpawning = false;
    }
    void SpawnEnemyAtPosition(Vector3 spawnPoint, Vector3 positionEnemy)
    {
        IEnemy rightEnemy = container.InstantiatePrefabForComponent<IEnemy>(enemyPrefab, spawnPoint, Quaternion.Euler(90, 0, 0), transform);
        rightEnemy.SetTargetPosition(positionEnemy);
    }
    void OnDestroy()
    {
        // Отписываемся от подписки при уничтожении объекта, чтобы избежать утечек памяти (написал для подстраховки)
        if (updateSubscription != null)
        {
            updateSubscription.Dispose();
        }
    }
}