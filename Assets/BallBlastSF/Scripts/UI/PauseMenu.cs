using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool pauseGame; //���� ��������� ����
    [SerializeField] private GameObject pauseMenu; //������ �� UI ����
    [SerializeField] private GameObject mainMenu; //������ �� ������� UI ����
    [SerializeField] private GameObject progressPanel; //������ �� ������ ���������
    [SerializeField] private GameObject LossMenu; //������ �� ������ ���������
    [SerializeField] private GameObject levelMenu; //������ �� ������ ������
    [SerializeField] private GameObject storeMenu; //������ �� ������ ��������

    void Update() //��������� ������� ������ ESC
    {
        if (LossMenu.activeSelf || levelMenu.activeSelf || mainMenu.activeSelf || storeMenu.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGame == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume() //���������� ���� � �������� ���� �����
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1f;

        pauseGame = false;
    }

    public void Pause() //������ ���� �� �����
    {
        pauseMenu.SetActive(true);

        Time.timeScale = 0f;

        pauseGame = true;
    }

    public void MainMenu() //��������� ������� ����
    {
        pauseMenu.SetActive(false);
        mainMenu.SetActive(true);
        progressPanel.SetActive(false);
        storeMenu.SetActive(false);

        Time.timeScale = 0f;

        pauseGame = false;
    }
}
