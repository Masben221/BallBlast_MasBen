using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel; //Ссылка на главное UI меню
    [SerializeField] private SceneHelper sceneHelper;
    [SerializeField] private LossMenu lossMenu; //Ссылка на UI меню проигрыша
    [SerializeField] private GameObject lossMenuPanel; //Ссылка на UI меню проигрыша
    [SerializeField] private GameObject progressPanel; //Ссылка на панель прогресса
    [SerializeField] private GameObject storeMenuPanel; //Ссылка на панель магазина
    [SerializeField] private UpgradeMenu upgradeMenu;

    public static bool IsActiv = true;

    private void Awake()
    {
        //Убирает шкалу прогресса если главное меню активно
        if (IsActiv == true)
        {
            mainMenuPanel.SetActive(true);
            progressPanel.SetActive(false);
            storeMenuPanel.SetActive(false);
            upgradeMenu.UpdateMenuText();

            Time.timeScale = 0f;
        }
        else
        {
            mainMenuPanel.SetActive(false);
            progressPanel.SetActive(true);
            storeMenuPanel.SetActive(false);
            upgradeMenu.UpdateMenuText();
        }

        IsActiv = false;
    }
    public void Play() //Продолжает игру и скрывает меню паузы
    {
        mainMenuPanel.SetActive(false);
        progressPanel.SetActive(true);
        upgradeMenu.UpdateMenuText();

        if (lossMenu.IsActive)
        {
            lossMenuPanel.SetActive(true);
            upgradeMenu.UpdateMenuText();
        }

        Time.timeScale = 1f;
    }
    public void Exit()  //Выход из игры
    {
        sceneHelper.ExitGame();
    }
        
    public void Reset() //Сброс игры
    {
        PlayerPrefs.DeleteAll();

        mainMenuPanel.SetActive(false);
        progressPanel.SetActive(true);

        Time.timeScale = 1f;

        sceneHelper.RestartLevel();
    }
}
