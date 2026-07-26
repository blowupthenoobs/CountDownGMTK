using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] songs;

    int lastSong = -1;

    // Start is called before the first frame update
    void Start()
    {
        RandomSong();
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(gameObject);
    }

    void RandomSong()
    {
        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, songs.Length);
        }
        while (randomIndex == lastSong);

        lastSong = randomIndex;

        audioSource.clip = songs[randomIndex];
        audioSource.Play();
    }
}
