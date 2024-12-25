using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float moveSpeed = 50f;
    float rotateSpeed = 10f;
    // [SerializeField] AudioClip tankerMovingAudio;
    [SerializeField] AudioClip tankerStartingSound;
    [SerializeField] AudioClip collisionSound;

    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(tankerStartingSound);
    }

    // Update is called once per frame
    void Update()
    {
        RotationProcess();
        MovementProcess();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(collisionSound);
            }
        }
    }

    private void RotationProcess()
    {
        // float mouseX = Input.GetAxis("Mouse X");

        // transform.Rotate(Vector3.up * mouseX * rotationSpeed * Time.deltaTime);
        // if(Input.GetKey(KeyCode.H) && !audioSource.isPlaying)
        // {
        //     audioSource.PlayOneShot(tankerMovingAudio);
        // }
        float yValue = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        transform.Rotate(0, yValue, 0);
    }

    private void MovementProcess()
    {
        //float xValue = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float zValue = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(0, 0, zValue);
    }
}
