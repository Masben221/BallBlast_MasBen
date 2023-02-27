using UnityEngine;

public class LossMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu; //Ссылка на главное UI меню
    [SerializeField] private GameObject lossMenu; //Ссылка на главное UI меню
    [SerializeField] private StoneSpawner stoneSpawner;
    [SerializeField] private SceneHelper sceneHelper;
    [SerializeField] private GameObject progressPanel; //Ссылка на панель прогресса
    [SerializeField] private UpgradeMenu upgradeMenu;

    private bool isActive;

    public bool IsActive { get => isActive; set => isActive = value; }
    
    public void Restart() //Рестарт игры и скрывает меню паузы
    {
        lossMenu.SetActive(false);

        Time.timeScale = 1f;

        stoneSpawner.AmountSpawner = 0;

        IsActive = false;

        sceneHelper.RestartLevel();

        upgradeMenu.UpdateMenuText();
    }

    public void MainMenu() //Загружает главное меню
    {
        IsActive = true;

        lossMenu.SetActive(false);
        mainMenu.SetActive(true);
        progressPanel.SetActive(false);

        Time.timeScale = 0f;
    }
}
