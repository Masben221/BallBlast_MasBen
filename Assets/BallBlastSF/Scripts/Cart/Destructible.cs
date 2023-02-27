using UnityEngine;
using UnityEngine.Events;

public class Destructible : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnDestroyEvent;
    [HideInInspector] public UnityEvent OnDamageEvent;

    public int maxHealth; //Максимальное колличество жизней
    private int health; //Жизнь

    private bool isDestroyed = false; //Флаг уничтожения

    private void Start()
    {
        health = maxHealth;
        OnDamageEvent.Invoke();
    }

    public void OnTakeDamage(int damage) //Задает урон
    {
        health -= damage;

        OnDamageEvent.Invoke();

        if (health <= 0)
        {
            Kill();
        }
    }

    //Добавить жизней.
    public void AddHealth()
    {
        health += 10;
        if (health >= maxHealth) health = 100;
        OnDamageEvent.Invoke();
    }


    protected virtual void Kill() //Метод обработки уничтожения, с событием
    {
        if (isDestroyed == true) return;
        health = 0;
        isDestroyed = true;

        OnDestroyEvent.Invoke();
    }

    public int GetHealth() //Получить колличество жизней
    {
        return health;
    }
}
