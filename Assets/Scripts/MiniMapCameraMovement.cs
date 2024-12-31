using UnityEngine;

public class MiniMapCameraMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 newPosition = target.transform.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition;
        }
    }
}
