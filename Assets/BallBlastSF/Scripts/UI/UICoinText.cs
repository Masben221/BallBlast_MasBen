using UnityEngine;
using UnityEngine.UI;

public class UICoinText : MonoBehaviour
{
    [SerializeField] private Bag bag;
    [SerializeField] private Text text;


    //подписка на событие сбора монеты
    private void Start()
    {
        bag.onPushCoin.AddListener(UpdateCoinText);
    }

    //отписка от события сбора монеты при ее уничтожении
    private void OnDestroy()
    {
        bag.onPushCoin.RemoveListener(UpdateCoinText);
    }

    //Обновляет текстовое поле монет при событии добавления в сумку и при вызове метода из Bag.
    public void UpdateCoinText()
    {
        text.text = bag.GetAmountCoin().ToString();
    }
}

