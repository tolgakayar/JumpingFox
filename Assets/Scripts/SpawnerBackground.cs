using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBackground : MonoBehaviour
{
    //Generate Sky:
    //Sk: x:4, y: 1.4, z:24, order in layer : -105,
    //Mount: x:4, y:-3.3, z: -116.3, order in layer : -100,
    [Header("Sky Properties")]
    [SerializeField] GameObject sky;
    
    [SerializeField] List<GameObject> skyObjects;
    [SerializeField] List<GameObject> mountainObjects;

    List<GameObject> skyGeneratedObjects;
    List<GameObject> mountainGeneratedObjects;
    float currentXSkyPosLimit = 100f;
    float skyWidth = 240f;
    

    [Header("Column Properties")]
    [SerializeField] GameObject column;

    [SerializeField] List<GameObject> columnObjects;

    List<GameObject> columnGeneratedObjects;
    float columnWidth = 30f;

    int generationCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        skyGeneratedObjects = new List<GameObject>();
        mountainGeneratedObjects = new List<GameObject>();
        columnGeneratedObjects = new List<GameObject>();

        GenerateBackground(0f);
    }

    // Update is called once per frame
    void Update()
    {
        float xCameraPosition = Camera.main.transform.position.x;
        
        if(xCameraPosition > currentXSkyPosLimit)
        {
            currentXSkyPosLimit = currentXSkyPosLimit + skyWidth;
            GenerateBackground(skyWidth * generationCount);
        }
    }

    private void GenerateBackground(float xPos)
    {
        GenerateSky(xPos);
        GenerateColumns(xPos);

        generationCount++;
    }

    private void GenerateSky(float xPos)
    {
        //generate sky
        Vector3 newSkyPosition = new Vector3(xPos,
            2*skyObjects[0].transform.position.y + sky.transform.position.y,
            skyObjects[0].transform.position.z + sky.transform.position.z);

        var newSkyObject = Instantiate(skyObjects[0], newSkyPosition, Quaternion.identity);
        newSkyObject.transform.parent = sky.transform;
        
        skyGeneratedObjects.Add(newSkyObject);

        //generate mountains
        Vector3 newMountainPosition = new Vector3(xPos,
            2 * mountainObjects[0].transform.position.y + sky.transform.position.y,
            mountainObjects[0].transform.position.z + sky.transform.position.z);

        var newMountainObject = Instantiate(mountainObjects[0], newMountainPosition, Quaternion.identity);
        newMountainObject.transform.parent = sky.transform;
        
        mountainGeneratedObjects.Add(newMountainObject);
    }

    private void GenerateColumns(float xPos)
    {
        float startPosition = (generationCount * skyWidth) - (skyWidth / 2) + (columnWidth / 4);

        int objectCount = 16;
        int columnObjectIndex;
        
        for (int i=0;i<objectCount;i++)
        {
            columnObjectIndex = Random.Range(0, columnObjects.Count);
            float newXPosition = startPosition + (i*columnWidth/2) ;

            //generate column
            Vector3 newColumnPosition = new Vector3(newXPosition,
                columnObjects[columnObjectIndex].transform.position.y+ column.transform.position.y,
                columnObjects[columnObjectIndex].transform.position.z + column.transform.position.z);

            var newColumnObject = Instantiate(columnObjects[columnObjectIndex], newColumnPosition, Quaternion.identity);
            newColumnObject.transform.parent = column.transform;
            
            columnGeneratedObjects.Add(newColumnObject);
        }
    }
}