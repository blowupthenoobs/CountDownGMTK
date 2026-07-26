using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] AudioSource audio;

    [SerializeField] float minPitch;
    [SerializeField] float maxPitch;

    [SerializeField] float minVolume;
    [SerializeField] float maxVolume;

    // Start is called before the first frame update
    void Awake()
    {
        audio.volume = Random.Range(minVolume, maxVolume);
        audio.pitch = Random.Range(minPitch, maxPitch);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
