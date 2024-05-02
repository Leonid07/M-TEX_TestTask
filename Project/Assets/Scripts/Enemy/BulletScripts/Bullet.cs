using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class Bullet : MonoBehaviour, IBullet
{
    [Inject] Menu menu;
    [Inject] BulletData bulletData;

    void Start()
    {
        Observable.Timer(System.TimeSpan.FromSeconds(bulletData.timeDestroyObject)).Subscribe(_ => Destroy(gameObject)).AddTo(this);

        this.UpdateAsObservable()
            .Subscribe(_ => SetDrivingDirections())
            .AddTo(this);

        this.OnTriggerEnterAsObservable()
            .Where(other => Target(other))
            .Subscribe(other => menu.IsPlayerDestroy())
            .AddTo(this);
    }
    public void SetDrivingDirections()
    {
        transform.position += Vector3.down * bulletData.speed * Time.deltaTime;
    }

    public bool Target(Collider target)
    {
        return target.gameObject.CompareTag("Player");
    }
}