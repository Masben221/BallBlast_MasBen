using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cart : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float cartSpeed; // скорость перемещени€
    [SerializeField] private float vehicleWidth;

    [Header("Wheels")]
    [SerializeField] private Transform[] wheels;
    [SerializeField] private float wheelRadius;

    [HideInInspector] public UnityEvent CollisionStone;   

    private Vector3 movementTarget; // точка к которой повозка будет катитьс€

    private float deltaMovement;
    private float lastPositionX;

    public float CartSpeed { get => cartSpeed; set => cartSpeed = value; } // скорость перемещени€ - свойство

    private void Awake()
    {
        cartSpeed = PlayerPrefs.GetFloat("Cart:CartSpeed", 10f);       
    }
    private void Start()
    {
        movementTarget = transform.position;
    }

    private void Update()
    {
        Move();

        RotateWheel();
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        Stone stone = collision.transform.root.GetComponent<Stone>();

        if (stone != null) // ѕроверка столкновени€ с камнем
        {
            CollisionStone.Invoke(); // —обытие столкновени€ с камнем
        }
    }
    private void Move() // ƒвижение 
    {
        lastPositionX = transform.position.x;

        transform.position = Vector3.MoveTowards(transform.position, movementTarget, cartSpeed * Time.deltaTime);

        deltaMovement = transform.position.x - lastPositionX;
    }

    private void RotateWheel()
    {
        float angle = (180 * deltaMovement) / (Mathf.PI * wheelRadius * 2);

        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].Rotate(0, 0, -angle);
        }
    }

    public void SetMovementTarget(Vector3 target)// ”становить цель движени€
    {
        movementTarget = ClampMovementTarget(target);
    }

    private Vector3 ClampMovementTarget(Vector3 target)
    {

        float leftBorder = LevelBoundary.Instance.LeftBorder + vehicleWidth * 0.5f;
        float rightBorder = LevelBoundary.Instance.RightBorder - vehicleWidth * 0.5f;

        Vector3 movTarget = target;
        movTarget.z = transform.position.z;
        movTarget.y = transform.position.y;

        if (movTarget.x < leftBorder) movTarget.x = leftBorder;
        if (movTarget.x > rightBorder) movTarget.x = rightBorder;

        return movTarget;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position - new Vector3(vehicleWidth * 0.5f, 0.5f, 0), transform.position + new Vector3(vehicleWidth * 0.5f, -0.5f, 0));
    }
#endif
}
