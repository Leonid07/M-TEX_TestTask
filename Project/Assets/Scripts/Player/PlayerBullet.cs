using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class PlayerBullet : MonoBehaviour, IBullet
{
    [Inject] BulletData bulletData;
    [SerializeField] float damage = 10;// урон от попадания по противникам

    void Start()
    {
        Observable.Timer(System.TimeSpan.FromSeconds(bulletData.timeDestroyObject))
            .Subscribe(_ => Destroy(gameObject))
            .AddTo(this);

        this.OnTriggerEnterAsObservable()
            .Where(other => Target(other))
            .Subscribe(other =>
            {
                other.gameObject.GetComponent<Enemy>().GetHealth(damage);
                Destroy(gameObject);
            })
            .AddTo(this);

        this.UpdateAsObservable()
            .Subscribe(_ => SetDrivingDirections())
            .AddTo(this);
    }
    public void SetDrivingDirections()
    {
        transform.position += Vector3.up * bulletData.speed * Time.deltaTime;
    }

    public bool Target(Collider target)
    {
        return target.gameObject.CompareTag("Enemy");
    }
}