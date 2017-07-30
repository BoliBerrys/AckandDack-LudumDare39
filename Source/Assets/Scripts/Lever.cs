using System.Collections;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject openedLever;
    [SerializeField] private GameObject openedDoor;
    [SerializeField] private float camSpeedIncrease = 0.1f;
    private bool opened;
    private Camera cam;

	void Start ()
	{
	    cam = Camera.main;
		door = GameObject.FindGameObjectWithTag("Door");
	    door.tag = "Door1";
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.CompareTag("Player") || opened) return;
        
        if (!opened)
        {
            cam.GetComponent<CameraMovement>().cameraSpeed += camSpeedIncrease;
            opened = true;
            Instantiate(openedDoor, new Vector2(door.transform.position.x, door.transform.position.y - 0.5f), Quaternion.identity);
            door.SetActive(false);
            Instantiate(openedLever, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}