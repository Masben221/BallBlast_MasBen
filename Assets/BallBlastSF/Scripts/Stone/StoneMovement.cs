using System;
using UnityEngine;

public class StoneMovement : MonoBehaviour
{
    [SerializeField] private float gravity; //Гравитация
    [SerializeField] private float reboundSpeed; //Скорость отскока
    [SerializeField] private float horizontalSpeed; //Горизонтальная скорость
    [SerializeField] private float rotationSpeed; //Скорость вращения
    [SerializeField] private float gravityOffset; //Величина смещения от границы экрана для включения гравитации

    private bool UseGravity; //Флаг гравитации
    private Vector3 velocity; //Вектор направления движения камня
    private bool isFreezing; //Флаг заморозки
    public bool IsFreezing { get => isFreezing; set => isFreezing = value; } //Флаг заморозки
    private void Awake()
    {
        // Задает направление движения при создании объекта камня
        velocity.x = -Mathf.Sign(transform.position.x) * horizontalSpeed; 
        isFreezing = false;
    }

    private void Update()
    {
        if (isFreezing == true) return; //При заморозке движение камня не происходит

        TryEnableGravity();
        Move();
    }

    private void TryEnableGravity() //Включает гравитацию в зависимости от координат
    {
        //проверка местонахождения камня по левой и правой границе
        if (Math.Abs(transform.position.x) <= Math.Abs(LevelBoundary.Instance.LeftBorder) - gravityOffset)
        {
            UseGravity = true;
        }
    }

    private void Move() //Расчет движения камня
    {
        if (UseGravity == true)
        {
            velocity.y -= gravity * Time.deltaTime;
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }

        velocity.x = Mathf.Sign(velocity.x) * horizontalSpeed;

        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) //Расчет поведения траектории камня от столкновений с границами экрана
    {
        LevelEdge levelEdge = collision.GetComponent<LevelEdge>();

        if (levelEdge != null)
        {
            if(levelEdge.Type == EdgeType.Bottom)
            {
                velocity.y = reboundSpeed;
            }

            if (levelEdge.Type == EdgeType.Left && velocity.x < 0 || levelEdge.Type == EdgeType.Right && velocity.x > 0)
            {
                velocity.x *= -1;
            }
        }
    }

    //Добавляет вертикальную силу вверх при делении камней
    public void AddVerticalVelocity(float velocity)
    {
        this.velocity.y += velocity;
    }

    //Задает направление движения при делении на два камня
    public void SetHorizontalDirection(float direction)
    {
        velocity.x = Mathf.Sign(direction) * horizontalSpeed;
    }
}
