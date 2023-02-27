using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu; //Ссылка на главное UI меню
    [SerializeField] private GameObject levelMenu; //Ссылка на меню следующий уровень
    [SerializeField] private StoneSpawner stoneSpawner;
    [SerializeField] private Bag bag;
    [SerializeField] private Turret turret;
    [SerializeField] private SceneHelper sceneHelper;
    [SerializeField] private LevelState levelState;
    [SerializeField] private UpgradeMenu upgradeMenu;
    [SerializeField] private GameObject progressPanel; //Ссылка на панель прогресса
    [SerializeField] private GameObject storePanel; //Ссылка на панель магазина    

    public void NextLevel() //Продолжает игру и скрывает меню паузы
    {
        levelMenu.SetActive(false);
        progressPanel.SetActive(true);
        Time.timeScale = 1f;
        stoneSpawner.CurrentLevel++;
        stoneSpawner.AmountSpawner = 0;
        PlayerPrefs.SetInt("LevelMenu:CurrentLevel", stoneSpawner.CurrentLevel);
        PlayerPrefs.SetInt("LevelMenu:AmountCoin", bag.GetAmountCoin());
        upgradeMenu.UpdateMenuText();

        sceneHelper.RestartLevel();
    }   

    public void MainMenu()  //Загружает главное меню
    {
        levelMenu.SetActive(false);
        mainMenu.SetActive(true);
        progressPanel.SetActive(false);
        Time.timeScale = 0f;
        upgradeMenu.UpdateMenuText();
    }
    
    public void StoreMenu() //Загружает меню магазина
    {
        levelState.IsMenuStore = true;
        levelMenu.SetActive(false);
        storePanel.SetActive(true);
        progressPanel.SetActive(true);
        Time.timeScale = 1f;
    }
}
