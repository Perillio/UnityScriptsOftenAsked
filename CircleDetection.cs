using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Detects GameObjects in Range (only Gameobjects with collider !!!)
public class CircleDetection : MonoBehaviour
{
    public float detectionRange = 50f;
    public float detectionTimer = 1f;
    public List<GameObject> detectedObjects = new List<GameObject>();

    private float timer;

    void Start()
    {
        timer = detectionTimer;
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            DetectionTick();
            timer = detectionTimer;
        }
    }
    void DetectionTick()
    {
        detectedObjects = new List<GameObject>();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != transform.gameObject)
            {
                detectedObjects.Add(hitCollider.gameObject);
            }
        }
    }
}
