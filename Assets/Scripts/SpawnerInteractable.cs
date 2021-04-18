using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerInteractable : MonoBehaviour
{
    [Header("Interactable Properties")]
    [SerializeField] List<GameObject> interactableObjects;
    [SerializeField] int interactableCount = 15;

    List<GameObject> interactableGeneratedObjects;

    //generation parameters
    int generationCount = 0;
    float currentXSkyPosLimit = 100f;

    float generationArea = 240f;
    float constantPadding = 50f;

    // Start is called before the first frame update
    void Start()
    {
        interactableGeneratedObjects = new List<GameObject>();

        GenerateInteractable(0f);
    }

    // Update is called once per frame
    void Update()
    {

        float xCameraPosition = Camera.main.transform.position.x;

        if (xCameraPosition > currentXSkyPosLimit)
        {
            currentXSkyPosLimit = currentXSkyPosLimit + generationArea;
            GenerateInteractable(generationArea * generationCount);
        }
    }

    private void GenerateInteractable(float xPos)
    {
        float startPosition = (generationCount * generationArea) - (generationArea / 2) + constantPadding;

        int interactableObjectIndex;
        float generationDistance = generationArea / interactableCount;

        for (int i = 0; i < interactableCount; i++)
        {
            interactableObjectIndex = Random.Range(0, interactableObjects.Count);
            float newXPosition = startPosition + (i * generationDistance);

            Vector3 newInteractablePosition = new Vector3(newXPosition,
             interactableObjects[interactableObjectIndex].transform.position.y + transform.position.y,
             interactableObjects[interactableObjectIndex].transform.position.z + transform.position.z);


            var newInteractableObject = Instantiate(interactableObjects[interactableObjectIndex], newInteractablePosition, Quaternion.identity);
            newInteractableObject.transform.parent = transform;

            interactableGeneratedObjects.Add(newInteractableObject);

            //Destroy object
            if (generationCount > 1)
            {
                //Destroy Interactable object
                Destroy(interactableGeneratedObjects[0]);
                interactableGeneratedObjects.RemoveAt(0);
            }
        }
    }
}
