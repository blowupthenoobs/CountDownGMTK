using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] songs;

    int lastSong = -1;
    SceneChanger changer;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        RandomSong();
    }

    // Update is called once per frame
    void Update()
    {
        changer = FindFirstObjectByType<SceneChanger>();

        audioSource.mute = changer != null && changer.isOnElevatorScene;
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
