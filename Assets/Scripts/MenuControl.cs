using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
    Quit();
    MainMenu();
    }
    void MainMenu()
    {
      if (Input.GetKey(KeyCode.Space))
      {
        SceneManager.LoadScene(1); 
      } 
    }
    void Quit()
    {
      if (Input.GetKeyDown(KeyCode.Q))  // если нажата клавиша Q 
       {
         Application.Quit(); // закрыть приложение
         Debug.Log("Нажал кнопку выхода");   
       }
      if (Input.GetKeyDown(KeyCode.Escape))  // если нажата клавиша Escape
       {
         SceneManager.LoadScene(0);    // загрузить главное меню
       }
    }
}
