using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpecificSceneButton : MonoBehaviour
{
    [Header("Настройки сцены")]
    [Tooltip("Имя сцены для загрузки")]
    public string targetSceneName = "";
    
    [Tooltip("ИЛИ индекс сцены для загрузки (если имя не указано)")]
    public int targetSceneIndex = -1;
    
    private void Start()
    {
        // Находим компонент Button на этом же GameObject
        Button button = GetComponent<Button>();
        
        // Добавляем обработчик события нажатия
        if (button != null)
        {
            button.onClick.AddListener(LoadSpecificScene);
        }
        else
        {
            Debug.LogError("Скрипт SpecificSceneButton должен быть на объекте с компонентом Button!");
        }
    }
    
    public void LoadSpecificScene()
    {
        // Проверяем, указано ли имя сцены
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            // Загружаем сцену по имени
            SceneManager.LoadScene(targetSceneName);
        }
        // Иначе проверяем, указан ли индекс сцены
        else if (targetSceneIndex >= 0)
        {
            // Проверяем, существует ли сцена с таким индексом
            if (targetSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(targetSceneIndex);
            }
            else
            {
                Debug.LogError($"Сцены с индексом {targetSceneIndex} не существует!");
            }
        }
        else
        {
            Debug.LogError("Не указано имя сцены или индекс!");
        }
    }
}