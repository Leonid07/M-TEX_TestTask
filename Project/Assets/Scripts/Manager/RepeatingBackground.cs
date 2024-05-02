using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    [Tooltip("Значение высоты 2D объекта, для передвижение его по оси Y если дойдёт по порога")]
    [SerializeField] float verticalSize;

    private void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => transform.position.y < -verticalSize)
            .Subscribe(_ => RepositionBackground())
            .AddTo(this);
    }

    void RepositionBackground()
    {
        Vector3 groundOffSet = new Vector3(0, verticalSize * 2f, 0);
        transform.position += groundOffSet;
    }
}