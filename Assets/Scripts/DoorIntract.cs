using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteract : MonoBehaviour
{
    public float openAngle = 90f;
    public float speed = 2f;
    public string nextSceneName;

    private bool isOpen = false;
    private bool playerNear = false;

    private Quaternion closedRot;
    private Quaternion openRot;

    void Start()
    {
        closedRot = transform.rotation;
        openRot = Quaternion.Euler(0, openAngle, 0) * closedRot;
    }

    void Update()
    {
        // Smooth rotation
        Quaternion target = isOpen ? openRot : closedRot;
        transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * speed);

        // Press E to interact
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;

            // If opening → go to next scene after short delay
            if (isOpen && !string.IsNullOrEmpty(nextSceneName))
            {
                Invoke(nameof(LoadNextScene), 1.5f);
            }
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNear = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNear = false;
    }
}