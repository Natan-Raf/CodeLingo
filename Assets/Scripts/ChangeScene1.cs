using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    public string targetSceneName;
    public bool useSceneIndex = false;
    public int targetSceneIndex = -1;
    public string playerTag = "Player";
    public bool savePlayerPosition = false;
    public string positionSaveKey = "PlayerPosition";
    public float transitionDelay = 0.5f;
    public GameObject transitionEffect;
    public AudioClip transitionSound;
    private bool isTransitioning = false;
    private GameObject player;

    void Start()
    {
        if (string.IsNullOrEmpty(playerTag))
        {
            playerTag = "Player";
        }

        if (useSceneIndex && targetSceneIndex >= 0)
        {
            CheckSceneIndex();
        }
    }

    void CheckSceneIndex()
    {
        if (targetSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError($"Индекс сцены {targetSceneIndex} выходит за пределы! Максимальный индекс: {SceneManager.sceneCountInBuildSettings - 1}");
            targetSceneIndex = -1;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTransitioning && other.CompareTag(playerTag))
        {
            player = other.gameObject;
            StartTransition();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTransitioning && collision.gameObject.CompareTag(playerTag))
        {
            player = collision.gameObject;
            StartTransition();
        }
    }

    void StartTransition()
    {
        if (isTransitioning) return;

        isTransitioning = true;

        if (savePlayerPosition && player != null)
        {
            SavePlayerPosition();
        }

        if (transitionEffect != null)
        {
            Instantiate(transitionEffect, transform.position, Quaternion.identity);
        }

        if (transitionSound != null)
        {
            AudioSource.PlayClipAtPoint(transitionSound, transform.position);
        }

        Invoke("LoadNextScene", transitionDelay);
    }

    void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat(positionSaveKey + "_X", player.transform.position.x);
        PlayerPrefs.SetFloat(positionSaveKey + "_Y", player.transform.position.y);
        PlayerPrefs.Save();
    }

    void LoadNextScene()
    {
        if (useSceneIndex && targetSceneIndex >= 0)
        {
            SceneManager.LoadScene(targetSceneIndex);
        }
        else if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.LogWarning("Нет следующей сцены! Возвращаемся к первой.");
                SceneManager.LoadScene(0);
            }
        }
    }
    public void ManualTriggerTransition(GameObject triggeringPlayer = null)
    {
        if (triggeringPlayer != null)
        {
            player = triggeringPlayer;
        }

        if (!isTransitioning)
        {
            StartTransition();
        }
    }
}