using System;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Camera cam;
    private Notification not;
    [SerializeField] private GameObject[] tiles;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material nightMaterial;
    [SerializeField] private GameObject nightBackground;
    [SerializeField] private GameObject dayBackground;
    private AudioSource audio;
    private GameObject levelLoader;

	void Start ()
	{
	    cam = Camera.main;
	    not = FindObjectOfType<Notification>();
	    audio = GetComponent<AudioSource>();
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader");
	    
        HelloNight();
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.CompareTag("Player")) return;
        if (gameObject.tag == "Battery")
        {
            audio.Play();
            not.Show();
            HelloLight();
            gameObject.SetActive(false);
        }
    }

    //Almost killed myself after coding this.
    // If it looks stupid but works, then it's not stupid.
    void HelloLight()
    {
        Instantiate(dayBackground);
        DestroyImmediate(nightBackground, true);
        cam.GetComponent<CameraMovement>().enableNight = false;
        cam.backgroundColor = new Color(210, 210, 210, 255);

        for (int i = 0; i < levelLoader.transform.childCount; i++)
        {
            foreach (Transform child in levelLoader.transform.GetChild(i))
            {
                if (child.transform.childCount > 0)
                {
                    for (int j = 0; j < child.transform.childCount; j++)
                    {
                        if (child.transform.GetChild(j).GetComponent<SpriteRenderer>() == null) continue;
                        var s = child.transform.GetChild(j).GetComponent<SpriteRenderer>();
                        s.enabled = true;
                        s.sharedMaterial = defaultMaterial;
                    }
                }
                else
                {
                    if (child.GetComponent<SpriteRenderer>() == null) continue;
                    var s = child.GetComponent<SpriteRenderer>();
                    s.enabled = true;
                    s.sharedMaterial = defaultMaterial;
                }
            }
        }
    }
    
    void HelloNight()
    {
        Instantiate(nightBackground);
        nightBackground.SetActive(true);
        cam.GetComponent<CameraMovement>().enableNight = true;
        cam.backgroundColor = Color.black;

        foreach (var tile in tiles)
        {
            for (int i = 0; i < tile.transform.childCount; i++)
            {
                if (tile.transform.GetChild(i).GetComponent<Renderer>() == null) continue;
                var s = tile.transform.GetChild(i).GetComponent<Renderer>();
                s.enabled = true;
                s.sharedMaterial = defaultMaterial;
                
                if (tile.transform.GetChild(i).GetComponent<SpriteRenderer>() == null) continue;
                var srender = tile.transform.GetChild(i).GetComponent<SpriteRenderer>();
                srender.material = nightMaterial;
            }
        }
    }
}