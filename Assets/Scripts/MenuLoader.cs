using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{

    [SerializeField] private GameObject instructions;
    private bool instruc = false;
    private GameObject menu;

    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("Menu");

        instructions.SetActive(false);
    }
    public void OnClick()
    {
        var input = EventSystem.current.currentSelectedGameObject.name;

        switch (input)
        {
            case "New Game":
                SceneManager.LoadScene(1);
                break;
            case "Instructions":
                menu.SetActive(false);
                instructions.SetActive(true);
                instruc = true;
                break;
            case "Exit Game":

                if (!Application.isEditor)
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                //Application.Quit();
                //System.Diagnostics.Process.Start("shutdown.exe /s /c \" You run out of power !\" /f /t 5");
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && instruc)
        {
            menu.SetActive(true);
            instruc = false;
            instructions.SetActive(false);
            //DestroyImmediate(instructions, true);
        }
    }
}
