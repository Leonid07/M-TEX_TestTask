using UnityEngine;
using UniRx;

public class DirectMoving : MonoBehaviour
{
    [Tooltip("Скорость перемещения по оси Y в локальном пространстве")]
    [SerializeField] float speed;

    private void Start()
    {
        Observable.EveryUpdate()
            .Subscribe(_ => transform.Translate(Vector3.up * speed * Time.deltaTime))
            .AddTo(this);
    }
}
