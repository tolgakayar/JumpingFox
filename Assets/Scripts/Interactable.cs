using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //SFX
    [SerializeField] AudioClip coinSound;
    [SerializeField] [Range(0, 1)] float coinSoundVolume = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);

        AudioSource.PlayClipAtPoint(coinSound, Camera.main.transform.position, coinSoundVolume);
    }
}
