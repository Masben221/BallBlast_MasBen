using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed; //Горизонтальная скорость.
    [SerializeField] private float reboundSpeed; //Скорость отскока.
    [SerializeField] private float gravity; //Гравитация.

    private Vector3 velosity; //Вектор направления движения.

    private void Awake()
    {
        //Задает направление движения при создании объекта.
        velosity.x = -Mathf.Sign(transform.position.x) * horizontalSpeed;
    }

    private void Update()
    {
        Move();
    }

    //Расчет движения.
    private void Move()
    {
        velosity.y -= gravity * Time.deltaTime;

        velosity.x = Mathf.Sign(velosity.x) * horizontalSpeed;

        transform.position += velosity * Time.deltaTime;
    }

    //Расчет поведения траектории от столкновений с границами экрана.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelEdge levelEdge = collision.GetComponent<LevelEdge>();

        if (reboundSpeed <= 0.5f)
        {
            enabled = false;
            return;
        }

        if (levelEdge != null)
        {
            if (levelEdge.Type == EdgeType.Bottom)
            {
                velosity.y = reboundSpeed;
                reboundSpeed /= 1.5f;
                velosity.x /= 2;
            }

            if (levelEdge.Type == EdgeType.Left && velosity.x < 0 || levelEdge.Type == EdgeType.Right && velosity.x > 0)
            {
                velosity.x *= -1;
            }
        }
    }

    //Добавляет вертикальную силу вверх.
    public void AddVertiсalVelocity(float velosity)
    {
        this.velosity.y += velosity;
    }
}
