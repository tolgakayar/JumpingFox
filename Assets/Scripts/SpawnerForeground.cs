using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerForeground : MonoBehaviour
{
    [Header("Stone Properties")]    
    [SerializeField] List<GameObject> stoneObjects;

    List<GameObject> stoneGeneratedObjects;
    float stoneWidth = 50f;
    

    [Header("Liana Properties")]
    [SerializeField] List<GameObject> lianaObjects;

    List<GameObject> lianaGeneratedObjects;
    float lianaWidth = 30f;

    int generationCount = 0;
    float currentXSkyPosLimit = 100f;

    //generation parameters
    float generationArea = 240f;
    float constantPadding = 10f;
    [SerializeField] int stoneCount = 8;
    [SerializeField] int lianaCount = 4;

    // Start is called before the first frame update
    void Start()
    {
        stoneGeneratedObjects = new List<GameObject>();
        lianaGeneratedObjects = new List<GameObject>();

        GenerateForeground(0f);
    }

    // Update is called once per frame
    void Update()
    {
        float xCameraPosition = Camera.main.transform.position.x;
        
        if(xCameraPosition > currentXSkyPosLimit)
        {
            currentXSkyPosLimit = currentXSkyPosLimit + generationArea;
            GenerateForeground(generationArea * generationCount);
        }
    }

    private void GenerateForeground(float xPos)
    {
        GenerateStones(xPos);
        GenerateLianas(xPos);

        generationCount++;
    }

    private void GenerateStones(float xPos)
    {
        float startPosition = (generationCount * generationArea) - (generationArea / 2) + constantPadding;

        int stoneObjectIndex;
        float generationDistance = generationArea / stoneCount;

        for (int i = 0; i < stoneCount; i++)
        {
            stoneObjectIndex = Random.Range(0, stoneObjects.Count);
            float newXPosition = startPosition + (i * generationDistance);

            Vector3 newStonePosition = new Vector3(newXPosition,
            stoneObjects[stoneObjectIndex].transform.position.y + transform.position.y,
            stoneObjects[stoneObjectIndex].transform.position.z + transform.position.z);

            var newStoneObject = Instantiate(stoneObjects[stoneObjectIndex], newStonePosition, Quaternion.identity);
            newStoneObject.transform.parent = transform;

            stoneGeneratedObjects.Add(newStoneObject);

            //Destroy object
            if (generationCount > 1)
            {
                //Destroy Stone object
                Destroy(stoneGeneratedObjects[0]);
                stoneGeneratedObjects.RemoveAt(0);
            }
        }
    }

    private void GenerateLianas(float xPos)
    {
        float startPosition = (generationCount * generationArea) - (generationArea / 2) + constantPadding;

        int lianaObjectIndex;
        float generationDistance = generationArea / lianaCount;

        for (int i=0;i<lianaCount;i++)
        {
            lianaObjectIndex = Random.Range(0, lianaObjects.Count);
            float newXPosition = startPosition + (i* generationDistance);

            Vector3 newLianaPosition = new Vector3(newXPosition,
            lianaObjects[lianaObjectIndex].transform.position.y + transform.position.y,
            lianaObjects[lianaObjectIndex].transform.position.z + transform.position.z);

            var newLianaObject = Instantiate(lianaObjects[lianaObjectIndex], newLianaPosition, Quaternion.identity);
            newLianaObject.transform.parent = transform;

            lianaGeneratedObjects.Add(newLianaObject);

            //Destroy object
            if (generationCount > 1)
            {
                //Destroy Liana object
                Destroy(lianaGeneratedObjects[0]);
                lianaGeneratedObjects.RemoveAt(0);
            }
        }
    }
}