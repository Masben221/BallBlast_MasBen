using UnityEngine;
using UnityEngine.Events;

public class Bag : MonoBehaviour
{
    [SerializeField] private UICoinText textCoinUI; //������ �� ��������� ���� �����.

    private int amountCoin; //������� �����

    public UnityEvent onPushCoin;

    private void Awake()
    {
        //��������� ������ �� ����� � ������� �� �����.
        amountCoin = PlayerPrefs.GetInt("LevelMenu:AmountCoin", 0);
        textCoinUI.UpdateCoinText();
    }

    //�������� ������
    public void PushCoin(int amount)
    {
        amountCoin += amount;

        onPushCoin.Invoke();
    }

    //�������� ����������� �����
    public int GetAmountCoin()
    {
        return amountCoin;
    }

    //����� ������
    public bool PullCoin(int amount)
    {
        if (amountCoin - amount < 0) return false;

        amountCoin -= amount;
        onPushCoin.Invoke();

        return true;
    }
}
