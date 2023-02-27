using UnityEngine;
using UnityEngine.Events;

public class Destructible : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnDestroyEvent;
    [HideInInspector] public UnityEvent OnDamageEvent;

    public int maxHealth; //������������ ����������� ������
    private int health; //�����

    private bool isDestroyed = false; //���� �����������

    private void Start()
    {
        health = maxHealth;
        OnDamageEvent.Invoke();
    }

    public void OnTakeDamage(int damage) //������ ����
    {
        health -= damage;

        OnDamageEvent.Invoke();

        if (health <= 0)
        {
            Kill();
        }
    }

    //�������� ������.
    public void AddHealth()
    {
        health += 10;
        if (health >= maxHealth) health = 100;
        OnDamageEvent.Invoke();
    }


    protected virtual void Kill() //����� ��������� �����������, � ��������
    {
        if (isDestroyed == true) return;
        health = 0;
        isDestroyed = true;

        OnDestroyEvent.Invoke();
    }

    public int GetHealth() //�������� ����������� ������
    {
        return health;
    }
}
