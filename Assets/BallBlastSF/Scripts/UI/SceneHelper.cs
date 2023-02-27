using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{
    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;       
    }
    
    public void RestartLevel() //Перезагрузка уровня
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }
        
    public void LoadLevel(int buildIndex) //Загрузка уровня
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void ExitGame() //Выход из игры
    {
        Application.Quit();
    }
}
