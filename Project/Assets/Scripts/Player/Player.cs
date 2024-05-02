using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [Inject(Id = "DestroyPlayer")] ParticleSystem destroyPlayer;

    public void GameOver()
    {
        Instantiate(destroyPlayer, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
