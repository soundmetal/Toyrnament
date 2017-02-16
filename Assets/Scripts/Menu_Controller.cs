using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnStartClick()
    {
        SceneManager.LoadScene("Main");//carga la escena primer nivel del juego
    }

    public void OnOptionsClick()
    {
        SceneManager.LoadScene("Menu_opciones");//carga la escena opciones
    }

    public void OnExitClick()
    {
        print("Hola");
        Application.Quit();//sale del juego
    }


    public void OnBackClick()
    {
        SceneManager.LoadScene("Menu_inicio");//vuelve al menu inicio
    }

}
