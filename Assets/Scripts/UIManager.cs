using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Метод для перехода на другую сцену
    public void LoadScene(string MainScene)
    {
        SceneManager.LoadScene(MainScene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}