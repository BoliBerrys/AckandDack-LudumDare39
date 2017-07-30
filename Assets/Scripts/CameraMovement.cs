using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class CameraMovement : MonoBehaviour
{


    /*
     TODO:  - El resto de las escenas del menu. (Incluir que es AC y DC)
         
         
         
         
         */
    public float cameraSpeed = 0.1f;
    [SerializeField] private Image nightImage;
    [SerializeField] private int minNightTimer = 7;
    [SerializeField] private int maxNightTimer = 15;
    [SerializeField] private Animation night;
    [SerializeField] private AnimationClip startNight;
    [SerializeField] private AnimationClip endNight;
    public bool enableNight = true;
    [HideInInspector] public GameObject finishText;
    private float temp;
    private int interval = 4;
    [SerializeField] private int nightTime = 1;
    
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        temp += Time.deltaTime;
        if (temp >= interval && enableNight)
        {
            Night();
            temp = 0;
        }

        transform.position += Vector3.up * cameraSpeed * Time.deltaTime;
	}

    void Night()
    {
        var r = new Random();
        interval = r.Next(minNightTimer, maxNightTimer);

        StartCoroutine(StartEffect(nightTime));
    }

    IEnumerator StartEffect(int seconds)
    {
        StartNight();
        yield return new WaitForSeconds(seconds);
        EndNight();
    }

    void StartNight()
    {
        night.clip = startNight;
        night.Play();
    }

    void EndNight()
    {
        night.clip = endNight;
        night.Play();
    }


    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
        }
    }
}