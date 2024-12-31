using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    // GameObjects
    [SerializeField] GameObject audioSourceTwoGameObject;
    float moveSpeed = 50f;
    float rotateSpeed = 20f;
    [SerializeField] AudioClip tankerStartingSound;
    [SerializeField] AudioClip movingSound;
    [SerializeField] AudioClip victorySound;
    [SerializeField] GameOver gameOver;

    // CASH
    AudioSource audioSource;
    AudioSource audioSourceTwo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSourceTwo = audioSourceTwoGameObject.GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        audioSourceTwo.PlayOneShot(tankerStartingSound);
    }

    // Update is called once per frame
    void Update()
    {
        RotationProcess();
        MovementProcess();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("ParkingLine"))
        {
            audioSource.Stop();
            audioSourceTwo.PlayOneShot(victorySound);
            gameOver.WinWindowProcess();
        }
    }

    private void RotationProcess()
    {
        // float mouseX = Input.GetAxis("Mouse X");

        // transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);
        float yValue = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        transform.Rotate(0, yValue, 0);
    }

    private void MovementProcess()
    {
        if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") < 0 || Input.GetAxis("Horizontal") < 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(movingSound);
            }
        }

        else
        {
            audioSource.Stop();
        }

        //float xValue = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float zValue = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(0, 0, zValue);
    }

}
