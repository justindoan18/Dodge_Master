using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public GameObject objectToSpawn; // assign in inspector
    public Vector3 spawnPosition;
    public Vector3 targetPosition;
    private float verticalX, verticalY, horizontalX, horizontalY;
    public float spawnInterval = 1f;
    private float speed;

    private float timer = 0f;

    void Start()
    {
        Camera camera = Camera.main;
        //Get spawn range
        verticalX = camera.aspect* camera.orthographicSize;
        verticalY = camera.orthographicSize + 1f;
        horizontalX = camera.aspect * camera.orthographicSize + 1f;
        horizontalY = camera.orthographicSize;

        Debug.Log(verticalX + "-" + verticalY);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer -= spawnInterval;
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        //Generate random point to spawn
        int conor = Random.Range(1,5);
        if(Mathf.Abs(conor) == 1)
        {
            //Left side
            spawnPosition = new Vector3(- horizontalX, Random.Range(-horizontalY, horizontalY), 0);
            targetPosition = new Vector3(horizontalX, Random.Range(-horizontalY, horizontalY), 0);
            
        } else 
        if (Mathf.Abs(conor) == 2)
        {
            //Up side
            spawnPosition = new Vector3(Random.Range(-verticalX, verticalX), verticalY, 0);
            targetPosition = new Vector3(Random.Range(-verticalX, verticalX), -verticalY, 0);
        } else 
        if (Mathf.Abs(conor) == 3)
        {
            //Right side
            spawnPosition = new Vector3(horizontalX, Random.Range(-horizontalY, horizontalY), 0);
            targetPosition = new Vector3(-horizontalX, Random.Range(-horizontalY, horizontalY), 0);
        } else
        {
            //Down side
            spawnPosition = new Vector3(Random.Range(-verticalX, verticalX), -verticalY, 0);
            targetPosition = new Vector3(Random.Range(-verticalX, verticalX), verticalY, 0);
        }
        //Spawn
        GameObject newObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        StartCoroutine(MoveObject(newObject.transform, targetPosition, 5f));
    }

    IEnumerator MoveObject(Transform objectToMove, Vector2 targetPosition, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float normalizedTime = t / (duration * 50);
            objectToMove.position = Vector2.Lerp(objectToMove.position, targetPosition, normalizedTime);    
            //Rotate
            Quaternion  toRotation = Quaternion.LookRotation(Vector3.forward, targetPosition);
            objectToMove.rotation = toRotation;
            yield return null;
        }
        Destroy(objectToMove.gameObject);
    }
}
