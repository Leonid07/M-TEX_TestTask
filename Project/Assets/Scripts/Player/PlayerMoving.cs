using UniRx;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    [SerializeField] float speed = 10.0f; // скорость игрока
    [SerializeField] float border = 2f; // отступ от краёв камеры для того чтобы объект был в зоне видимости игроком
    [SerializeField] float tiltAmount = 10.0f; // степень наклона по время движения наклон а именно наклон по оси Z во время движения по горизонтали
    float minX, maxX, minY, maxY; // минимальные и максимальные значения X и Y для ограничения движения объекта
    Vector3 initialPosition; // начальная позиция объекта

    void Start()
    {
        Border();

        Observable.EveryUpdate()
            .Subscribe(_ =>
            {
                Vector3 touchPosition = transform.position;

                if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10)); // 10 - это расстояние от камеры
                    }
                }

                if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
                {
                    float moveHorizontal = Input.GetAxis("Horizontal");
                    float moveVertical = Input.GetAxis("Vertical");

                    Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
                    touchPosition = transform.position + movement;
                }

                Vector3 direction = (touchPosition - transform.position).normalized;
                transform.Translate(direction * speed * Time.deltaTime, Space.World);

                float tilt = direction.x * -tiltAmount;
                Quaternion targetRotation = Quaternion.Euler(-90, tilt, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * speed);

                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, minX, maxX),
                    Mathf.Clamp(transform.position.y, minY, maxY),
                    initialPosition.z
                );
            })
            .AddTo(this);
    }


    void Border()
    {
        initialPosition = transform.position;
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance - border));
        Vector3 rightBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance - border));
        Vector3 bottomBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance - border));
        Vector3 topBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance - border));
        minX = leftBound.x;
        maxX = rightBound.x;
        minY = bottomBound.y;
        maxY = topBound.y;
    }
}
