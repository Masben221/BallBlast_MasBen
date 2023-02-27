using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel; //������ �� ������� UI ����
    [SerializeField] private SceneHelper sceneHelper;
    [SerializeField] private LossMenu lossMenu; //������ �� UI ���� ���������
    [SerializeField] private GameObject lossMenuPanel; //������ �� UI ���� ���������
    [SerializeField] private GameObject progressPanel; //������ �� ������ ���������
    [SerializeField] private GameObject storeMenuPanel; //������ �� ������ ��������
    [SerializeField] private UpgradeMenu upgradeMenu;

    public static bool IsActiv = true;

    private void Awake()
    {
        //������� ����� ��������� ���� ������� ���� �������
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
    public void Play() //���������� ���� � �������� ���� �����
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
    public void Exit()  //����� �� ����
    {
        sceneHelper.ExitGame();
    }
        
    public void Reset() //����� ����
    {
        PlayerPrefs.DeleteAll();

        mainMenuPanel.SetActive(false);
        progressPanel.SetActive(true);

        Time.timeScale = 1f;

        sceneHelper.RestartLevel();
    }
}
