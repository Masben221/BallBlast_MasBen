using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILevelProgress : MonoBehaviour
{
    [SerializeField] private StoneSpawner stoneSpawner;

    [SerializeField] private Text currentLevelText; //—сылка на текстовое поле текщего уровн€
    [SerializeField] private Text nextLevelText;//—сылка на текстовое поле следующего уровн€
    [SerializeField] private Image progressBar; //—сылка на прогресс бар
    [SerializeField] private UnityEvent progressBarEvent;

    private float fillAmountStep; //Ўаг в прогресс баре

    public float FillAmountStep { get => fillAmountStep; set => fillAmountStep = value; } //Ўаг в прогресс баре - свойство

    private void Start()
    {
        currentLevelText.text = stoneSpawner.CurrentLevel.ToString();
        nextLevelText.text = (stoneSpawner.CurrentLevel + 1).ToString();
        progressBar.fillAmount = 0f;

        FillAmountStep = (float)1f / (stoneSpawner.ProgressCountStone);
    }

    //ќбновление прогресс бара с вызовом событи€
    public void UpdateProgressBar()
    {
        progressBar.fillAmount += FillAmountStep;
        progressBarEvent.Invoke();
    }
}
