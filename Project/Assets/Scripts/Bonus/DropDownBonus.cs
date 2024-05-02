using UnityEngine;

public class DropDownBonus : MonoBehaviour
{
    [System.Serializable]
    public struct Bonus
    {
        public GameObject prefab; // Префаб бонуса
        public int dropChance; // Шанс выпадения бонуса
    }

    public Bonus[] bonuses; // Массив бонусов

    public void SpawnBonus()
    {
        // Выбираем бонус с учетом вероятности выпадения
        GameObject bonusToSpawn = GetRandomBonus();

        if (bonusToSpawn != null)
        {
            Instantiate(bonusToSpawn, transform.position, Quaternion.identity);
        }
    }

    GameObject GetRandomBonus()
    {
        foreach (Bonus bonus in bonuses)
        {
            if (Random.Range(0, 100) < bonus.dropChance)
            {
                return bonus.prefab; // Если бонус выбран, прерываем цикл
            }
        }

        return null;
    }
}
