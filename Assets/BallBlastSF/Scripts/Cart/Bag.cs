using UnityEngine;
using UnityEngine.Events;

public class Bag : MonoBehaviour
{
    [SerializeField] private UICoinText textCoinUI; //Ссылка на текстовое поле монет.

    private int amountCoin; //Счетчик монет

    public UnityEvent onPushCoin;

    private void Awake()
    {
        //Загружает монеты из файла и выводит на экран.
        amountCoin = PlayerPrefs.GetInt("LevelMenu:AmountCoin", 0);
        textCoinUI.UpdateCoinText();
    }

    //Положить монету
    public void PushCoin(int amount)
    {
        amountCoin += amount;

        onPushCoin.Invoke();
    }

    //Получить колличество монет
    public int GetAmountCoin()
    {
        return amountCoin;
    }

    //Взять монету
    public bool PullCoin(int amount)
    {
        if (amountCoin - amount < 0) return false;

        amountCoin -= amount;
        onPushCoin.Invoke();

        return true;
    }
}
