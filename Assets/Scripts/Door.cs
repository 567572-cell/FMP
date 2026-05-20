using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door Settings")]
    public Animator animator;

    [Header("Interaction")]
    public KeyCode interactKey = KeyCode.E;

    public GameObject  door;

    private bool playerNear = false;
    private bool isOpen = false;

    void Update()
    {
        // Player presses E near the door
        if (playerNear )  //&& Input.GetKeyDown(interactKey)
        {
          
        }
    }

    void ToggleDoor()
    {
        isOpen = !isOpen;

        // Sends bool to Animator
        animator.SetBool("IsOpen", isOpen);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            Debug.Log("Player near door" + other.gameObject.name);

            //animator.SetTrigger("Open");

            //door.SetActive(false);

            

           ToggleDoor();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            Debug.Log("Player left door" + other.gameObject.name);

            //animator.SetTrigger("Close");

            //door.SetActive(true);

            ToggleDoor();
        }
    }
}