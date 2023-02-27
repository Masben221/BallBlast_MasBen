using UnityEngine;
using UnityEngine.UI;

public class UICoinText : MonoBehaviour
{
    [SerializeField] private Bag bag;
    [SerializeField] private Text text;


    //�������� �� ������� ����� ������
    private void Start()
    {
        bag.onPushCoin.AddListener(UpdateCoinText);
    }

    //������� �� ������� ����� ������ ��� �� �����������
    private void OnDestroy()
    {
        bag.onPushCoin.RemoveListener(UpdateCoinText);
    }

    //��������� ��������� ���� ����� ��� ������� ���������� � ����� � ��� ������ ������ �� Bag.
    public void UpdateCoinText()
    {
        text.text = bag.GetAmountCoin().ToString();
    }
}

