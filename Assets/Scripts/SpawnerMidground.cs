using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMidground : MonoBehaviour
{
    [Header("Mass Properties")]    
    [SerializeField] List<GameObject> massObjects;

    List<GameObject> massGeneratedObjects;
    
    int generationCount = 0;
    float currentXSkyPosLimit = 100f;

    //generation parameters
    float generationArea = 240f;
    float constantPadding = 10f;
    [SerializeField]int massCount = 15;

    // Start is called before the first frame update
    void Start()
    {
        massGeneratedObjects = new List<GameObject>();

        GenerateMidground(0f);
    }

    // Update is called once per frame
    void Update()
    {
        float xCameraPosition = Camera.main.transform.position.x;
        
        if(xCameraPosition > currentXSkyPosLimit)
        {
            currentXSkyPosLimit = currentXSkyPosLimit + generationArea;
            GenerateMidground(generationArea * generationCount);
        }
    }

    private void GenerateMidground(float xPos)
    {
        GenerateMass(xPos);

        generationCount++;
    }

    private void GenerateMass(float xPos)
    {
        float startPosition = (generationCount * generationArea) - (generationArea / 2) + constantPadding;

        int massObjectIndex;
        float generationDistance = generationArea / massCount;

        for (int i = 0; i < massCount; i++)
        {
            massObjectIndex = Random.Range(0, massObjects.Count);
            float newXPosition = startPosition + (i * generationDistance);

            Vector3 newMassPosition = new Vector3(newXPosition,
            5*massObjects[massObjectIndex].transform.position.y + transform.position.y,
            massObjects[massObjectIndex].transform.position.z + transform.position.z);


            var newMassObject = Instantiate(massObjects[massObjectIndex], newMassPosition, Quaternion.identity);
            newMassObject.transform.parent = transform;

            massGeneratedObjects.Add(newMassObject);
        }
    }
}