using UnityEngine;

public class LossMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu; //������ �� ������� UI ����
    [SerializeField] private GameObject lossMenu; //������ �� ������� UI ����
    [SerializeField] private StoneSpawner stoneSpawner;
    [SerializeField] private SceneHelper sceneHelper;
    [SerializeField] private GameObject progressPanel; //������ �� ������ ���������
    [SerializeField] private UpgradeMenu upgradeMenu;

    private bool isActive;

    public bool IsActive { get => isActive; set => isActive = value; }
    
    public void Restart() //������� ���� � �������� ���� �����
    {
        lossMenu.SetActive(false);

        Time.timeScale = 1f;

        stoneSpawner.AmountSpawner = 0;

        IsActive = false;

        sceneHelper.RestartLevel();

        upgradeMenu.UpdateMenuText();
    }

    public void MainMenu() //��������� ������� ����
    {
        IsActive = true;

        lossMenu.SetActive(false);
        mainMenu.SetActive(true);
        progressPanel.SetActive(false);

        Time.timeScale = 0f;
    }
}
