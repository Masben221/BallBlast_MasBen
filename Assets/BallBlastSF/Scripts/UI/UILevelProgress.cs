using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILevelProgress : MonoBehaviour
{
    [SerializeField] private StoneSpawner stoneSpawner;

    [SerializeField] private Text currentLevelText; //������ �� ��������� ���� ������� ������
    [SerializeField] private Text nextLevelText;//������ �� ��������� ���� ���������� ������
    [SerializeField] private Image progressBar; //������ �� �������� ���
    [SerializeField] private UnityEvent progressBarEvent;

    private float fillAmountStep; //��� � �������� ����

    public float FillAmountStep { get => fillAmountStep; set => fillAmountStep = value; } //��� � �������� ���� - ��������

    private void Start()
    {
        currentLevelText.text = stoneSpawner.CurrentLevel.ToString();
        nextLevelText.text = (stoneSpawner.CurrentLevel + 1).ToString();
        progressBar.fillAmount = 0f;

        FillAmountStep = (float)1f / (stoneSpawner.ProgressCountStone);
    }

    //���������� �������� ���� � ������� �������
    public void UpdateProgressBar()
    {
        progressBar.fillAmount += FillAmountStep;
        progressBarEvent.Invoke();
    }
}
