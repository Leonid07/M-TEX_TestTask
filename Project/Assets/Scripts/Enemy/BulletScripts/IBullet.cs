
using UnityEngine;

public interface IBullet
{
    void SetDrivingDirections();
    bool Target(Collider target);
}
