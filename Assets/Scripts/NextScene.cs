using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextSceneButton : MonoBehaviour
{
    // Прикрепите этот скрипт к объекту с кнопкой (Button)

    private void Start()
    {
        // Находим компонент Button на этом же GameObject
        Button button = GetComponent<Button>();

        // Добавляем обработчик события нажатия
        if (button != null)
        {
            button.onClick.AddListener(LoadNextScene);
        }
        else
        {
            Debug.LogError("Скрипт NextSceneButton должен быть на объекте с компонентом Button!");
        }
    }

    public void LoadNextScene()
    {
        // Получаем индекс текущей сцены
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Вычисляем индекс следующей сцены
        int nextSceneIndex = currentSceneIndex + 1;

        // Проверяем, существует ли следующая сцена
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // Если это последняя сцена, возвращаемся к первой (0)
            SceneManager.LoadScene(0);
            Debug.Log("Это была последняя сцена. Возвращаемся к первой.");
        }
    }
}