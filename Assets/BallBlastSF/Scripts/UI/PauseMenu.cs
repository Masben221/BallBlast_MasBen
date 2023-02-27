using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool pauseGame; //Флаг активации меню
    [SerializeField] private GameObject pauseMenu; //Ссылка на UI меню
    [SerializeField] private GameObject mainMenu; //Ссылка на главное UI меню
    [SerializeField] private GameObject progressPanel; //Ссылка на панель прогресса
    [SerializeField] private GameObject LossMenu; //Ссылка на панель проигрыша
    [SerializeField] private GameObject levelMenu; //Ссылка на панель победы
    [SerializeField] private GameObject storeMenu; //Ссылка на панель магазина

    void Update() //Обработка нажатия кнопки ESC
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

    public void Resume() //Продолжает игру и скрывает меню паузы
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1f;

        pauseGame = false;
    }

    public void Pause() //Ставит игру на паузу
    {
        pauseMenu.SetActive(true);

        Time.timeScale = 0f;

        pauseGame = true;
    }

    public void MainMenu() //Загружает главное меню
    {
        pauseMenu.SetActive(false);
        mainMenu.SetActive(true);
        progressPanel.SetActive(false);
        storeMenu.SetActive(false);

        Time.timeScale = 0f;

        pauseGame = false;
    }
}
