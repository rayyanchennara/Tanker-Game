using UnityEngine;

public class LIneScript : MonoBehaviour
{
    LineRenderer lineRenderer;
    [SerializeField] Transform sphere;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        // Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10f);
        lineRenderer.SetPosition(1, sphere.position);
    }
}
