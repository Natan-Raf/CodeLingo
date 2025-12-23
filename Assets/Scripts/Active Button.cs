using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TriggerButtonController : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private Button targetButton;
    [SerializeField] private Collider triggerZone;
    [SerializeField] private string playerLayerName = "Player";

    private int playerLayer;

    IEnumerator Start()
    {
        playerLayer = LayerMask.NameToLayer(playerLayerName);

        if (targetButton == null || triggerZone == null)
        {
            Debug.LogError("References not set!");
            yield break;
        }

        // Ждем конец кадра, чтобы все объекты успели инициализироваться
        yield return new WaitForEndOfFrame();

        // Гарантируем, что кнопка скрыта после инициализации
        targetButton.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            targetButton.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            targetButton.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            targetButton.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            targetButton.gameObject.SetActive(false);
        }
    }
}
