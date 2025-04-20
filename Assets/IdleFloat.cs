using UnityEngine;

public class IdleFloat : MonoBehaviour
{
    public float floatStrength = 0.05f; // Seberapa tinggi
    public float speed = 2f; // Seberapa cepat

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * speed) * floatStrength;
    }
}
