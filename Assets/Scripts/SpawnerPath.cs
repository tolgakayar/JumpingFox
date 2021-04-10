using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPath : MonoBehaviour
{
    [Header("Ground Properties")]    
    [SerializeField] List<GameObject> groundObjects;
    [SerializeField] int groundCount = 15;

    List<GameObject> groundGeneratedObjects;

    [Header("Flower Properties")]
    [SerializeField] List<GameObject> flowerObjects;
    [SerializeField] int flowerCount = 15;

    List<GameObject> flowerGeneratedObjects;

    int generationCount = 0;
    float currentXSkyPosLimit = 100f;

    //generation parameters
    float generationArea = 240f;
    float constantPadding = 10f;
    

    // Start is called before the first frame update
    void Start()
    {
        groundGeneratedObjects = new List<GameObject>();
        flowerGeneratedObjects = new List<GameObject>();

        GeneratePath(0f);
    }

    // Update is called once per frame
    void Update()
    {
        float xCameraPosition = Camera.main.transform.position.x;
        
        if(xCameraPosition > currentXSkyPosLimit)
        {
            currentXSkyPosLimit = currentXSkyPosLimit + generationArea;
            GeneratePath(generationArea * generationCount);
        }
    }

    private void GeneratePath(float xPos)
    {
        GenerateGround(xPos);
        GenerateFlowers(xPos);

        generationCount++;
    }

    private void GenerateGround(float xPos)
    {
        float startPosition = (generationCount * generationArea) - (generationArea / 2) + constantPadding;

        int groundObjectIndex;
        float generationDistance = generationArea / groundCount;

        for (int i = 0; i < groundCount; i++)
        {
            groundObjectIndex = Random.Range(0, groundObjects.Count);
            float newXPosition = startPosition + (i * generationDistance);

            Vector3 newGroundPosition = new Vector3(newXPosition,
             groundObjects[groundObjectIndex].transform.position.y + transform.position.y,
             groundObjects[groundObjectIndex].transform.position.z + transform.position.z);


            var newGroundObject = Instantiate(groundObjects[groundObjectIndex], newGroundPosition, Quaternion.identity);
            newGroundObject.transform.parent = transform;

            groundGeneratedObjects.Add(newGroundObject);
        }
    }

    private void GenerateFlowers(float xPos)
    {
        float startPosition = (generationCount * generationArea) - (generationArea / 2) + constantPadding;

        int flowerObjectIndex;
        float generationDistance = generationArea / flowerCount;

        for (int i = 0; i < flowerCount; i++)
        {
            flowerObjectIndex = Random.Range(0, flowerObjects.Count);
            float newXPosition = startPosition + (i * generationDistance);

            Vector3 newFlowerPosition = new Vector3(newXPosition,
            5 * flowerObjects[flowerObjectIndex].transform.position.y + transform.position.y,
            flowerObjects[flowerObjectIndex].transform.position.z + transform.position.z);


            var newFlowerObject = Instantiate(flowerObjects[flowerObjectIndex], newFlowerPosition, Quaternion.identity);
            newFlowerObject.transform.parent = transform;

            flowerGeneratedObjects.Add(newFlowerObject);
        }
    }
}