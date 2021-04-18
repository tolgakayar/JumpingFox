using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObstacle : MonoBehaviour
{
    [Header("Obstacle Properties")]
    [SerializeField] List<GameObject> obstacleObjects;
    [SerializeField] int obstacleCount = 15;

    List<GameObject> obstacleGeneratedObjects;

    //generation parameters
    int generationCount = 0;
    float currentXSkyPosLimit = 100f;

    float generationArea = 240f;
    float constantPadding = 50f;

    // Start is called before the first frame update
    void Start()
    {
        obstacleGeneratedObjects = new List<GameObject>();

        GenerateObstacle(0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        float xCameraPosition = Camera.main.transform.position.x;

        if (xCameraPosition > currentXSkyPosLimit)
        {
            currentXSkyPosLimit = currentXSkyPosLimit + generationArea;
            GenerateObstacle(generationArea * generationCount);
        }
    }

    private void GenerateObstacle(float xPos)
    {
        float startPosition = (generationCount * generationArea) - (generationArea / 2) + constantPadding;

        int obstacleObjectIndex;
        float generationDistance = generationArea / obstacleCount;

        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleObjectIndex = Random.Range(0, obstacleObjects.Count);
            float newXPosition = startPosition + (i * generationDistance);

            Vector3 newObstaclePosition = new Vector3(newXPosition,
             obstacleObjects[obstacleObjectIndex].transform.position.y + transform.position.y,
             obstacleObjects[obstacleObjectIndex].transform.position.z + transform.position.z);


            var newObstacleObject = Instantiate(obstacleObjects[obstacleObjectIndex], newObstaclePosition, Quaternion.identity);
            newObstacleObject.transform.parent = transform;

            obstacleGeneratedObjects.Add(newObstacleObject);

            //Destroy object
            if (generationCount > 1)
            {
                //Destroy Obstacle object
                Destroy(obstacleGeneratedObjects[0]);
                obstacleGeneratedObjects.RemoveAt(0);
            }
        }
    }
}
