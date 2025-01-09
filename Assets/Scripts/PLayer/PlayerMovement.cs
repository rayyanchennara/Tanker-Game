using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variables")]
    float moveSpeed = 50f; // Speed of movement
    float rotateSpeed = 20f; // Speed of rotation

    [Header("Game Objects")]
    [SerializeField] GameObject audioSourceTwoGameObject; // GameObject containing the second AudioSource

    [Header("AudioClips")]
    [SerializeField] AudioClip tankerStartingSound; // Sound played when the tanker starts
    [SerializeField] AudioClip movingSound; // Sound played when the tanker is moving
    [SerializeField] AudioClip victorySound; // Sound played upon victory

    [Header("Canvas")]
    [SerializeField] GameOver gameOver; // Reference to the GameOver script

    [Header("Reference")]
    AudioSource audioSource; // Reference to the main AudioSource component
    AudioSource audioSourceTwo; // Reference to the second AudioSource component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSourceTwo = audioSourceTwoGameObject.GetComponent<AudioSource>(); // Get the second AudioSource component
        audioSource = GetComponent<AudioSource>(); // Get the main AudioSource component
        audioSource.Stop(); // Stop the main AudioSource at the start
        audioSourceTwo.PlayOneShot(tankerStartingSound); // Play the tanker starting sound
    }

    // Update is called once per frame
    void Update()
    {
        RotationProcess(); // Call the rotation process function
        MovementProcess(); // Call the movement process function
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("ParkingLine")) // Check if the collision is with an object tagged "ParkingLine"
        {
            audioSource.Stop(); // Stop the moving sound
            audioSourceTwo.PlayOneShot(victorySound); // Play the victory sound
            gameOver.WinWindowProcess(); // Call the win window process function in the GameOver script
        }
    }

    private void RotationProcess()
    {
        float yValue = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime; // Calculate rotation based on horizontal input
        transform.Rotate(0, yValue, 0); // Rotate the transform around the y-axis
    }

    private void MovementProcess()
    {
        if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") < 0 || Input.GetAxis("Horizontal") < 0) // Check if there is any horizontal or vertical input
        {
            if (!audioSource.isPlaying) // Check if the moving sound is not already playing
            {
                audioSource.PlayOneShot(movingSound); // Play the moving sound
            }
        }

        else
        {
            audioSource.Stop(); // Stop the moving sound if there is no input
        }

        float zValue = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime; // Calculate movement based on vertical input
        transform.Translate(0, 0, zValue); // Move the transform along the z-axis
    }
}
