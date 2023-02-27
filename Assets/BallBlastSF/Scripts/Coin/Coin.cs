using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bag bag = collision.transform.root.GetComponent<Bag>();

        if (bag)
        {
            //�������� ������ ��� ������������ � �������.
            bag.PushCoin(1);
            Destroy(gameObject);
        }
    }
}

