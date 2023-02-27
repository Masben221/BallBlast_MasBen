using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu; //������ �� ������� UI ����
    [SerializeField] private GameObject levelMenu; //������ �� ���� ��������� �������
    [SerializeField] private StoneSpawner stoneSpawner;
    [SerializeField] private Bag bag;
    [SerializeField] private Turret turret;
    [SerializeField] private SceneHelper sceneHelper;
    [SerializeField] private LevelState levelState;
    [SerializeField] private UpgradeMenu upgradeMenu;
    [SerializeField] private GameObject progressPanel; //������ �� ������ ���������
    [SerializeField] private GameObject storePanel; //������ �� ������ ��������    

    public void NextLevel() //���������� ���� � �������� ���� �����
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

    public void MainMenu()  //��������� ������� ����
    {
        levelMenu.SetActive(false);
        mainMenu.SetActive(true);
        progressPanel.SetActive(false);
        Time.timeScale = 0f;
        upgradeMenu.UpdateMenuText();
    }
    
    public void StoreMenu() //��������� ���� ��������
    {
        levelState.IsMenuStore = true;
        levelMenu.SetActive(false);
        storePanel.SetActive(true);
        progressPanel.SetActive(true);
        Time.timeScale = 1f;
    }
}
