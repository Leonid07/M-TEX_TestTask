using UnityEngine;

public interface IEnemy
{
    void SetTargetPosition(Vector3 target);
    void GetHealth(float health);
}
