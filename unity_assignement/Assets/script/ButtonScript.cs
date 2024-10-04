using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public string buttonType;
    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        if (buttonType == "resume") GetComponent<Button>().onClick.AddListener(resume);
        if (buttonType == "menu") GetComponent<Button>().onClick.AddListener(menu);
        if (buttonType == "quit") GetComponent<Button>().onClick.AddListener(quit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void resume() { character.GetComponent<PlayerController>().pauseFunction(); }

    void menu() { SceneManager.LoadScene("scenes/Menu"); }

    void quit() { Application.Quit(); }

}
