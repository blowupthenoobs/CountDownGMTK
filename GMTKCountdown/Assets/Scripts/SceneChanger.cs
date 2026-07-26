using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] string scene;
    [SerializeField] float timeBetweenSceneSwitch;
    public bool isOnElevatorScene;

    [SerializeField] GameObject dingSound;

    [SerializeField] float spawnTime;
    float timeBetweenSoundSwitch;

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LoadScene());

        timeBetweenSoundSwitch += Time.deltaTime;
        if(spawnTime <= timeBetweenSoundSwitch)
        {
            Instantiate(dingSound);
            timeBetweenSoundSwitch = 0;
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(timeBetweenSceneSwitch);
        SceneManager.LoadScene(scene);
    }
}
