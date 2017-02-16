using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class finalpartida : MonoBehaviour {
    public Text P1;
    public Text P2;
    public Text T1;
    public Text T2;

    
    public bool perder;
    public bool perder2;
    GameObject player;

   
    
    Scene SC;
   
   
    //cargo la escena en la que estas
    void Start()
    {
        perder = false;
        perder2 = false;
        
        SC = SceneManager.GetActiveScene();
    }

    //Se Comprueba la colision con el fondo
    public void OnTriggerEnter2D(Collider2D obj)
    {
        
        
        if (obj.gameObject.tag == "Player")
        {

            player = GameObject.Find("Player");
            perder = true;
            Destroy(obj.gameObject);
            final();

        }
        else if (obj.gameObject.tag == "Player2")
        {
            player = GameObject.Find("Player2");
            perder2 = true;
            Destroy(obj.gameObject);
            
            final();
        }
    }
        public Text getText()
        {
            return P1;
        }
        public Text getText2()
        {
            return P2;
        }
    //la condicion para mostrar el texto y parar el otro personaje 
    public void final()
    {
        if (perder)
        {
            P2.color = new Color(255, 255, 255);
            player = GameObject.Find("Player2");
            PlayerController_2.speed = 0;
            Contador.C2++;
            T2.text=""+Contador.C2;
            T1.text = "" + Contador.C1;
            StartCoroutine(CT());
            perder = false;
            


        }
        else if (perder2)
        {
            P1.color = new Color(255, 255, 255);
            player = GameObject.Find("Player");
            PlayerController.speed = 0;
            Contador.C1 ++;
            T1.text = ""+Contador.C1;
            T2.text = "" + Contador.C2;
            StartCoroutine(CT());
          
            perder2 = false;
        }
      
       
    }
 

    //se carga la siguiente escena
    IEnumerator CT()
    {
       
        yield return new WaitForSeconds(3);
        PlayerController.speed = 3.5f;
        PlayerController_2.speed = 3.5f;
        Contador.cScene++;
        if (Contador.cScene < 3)
            SceneManager.LoadScene(SC.buildIndex + 1);//si el contador de escenas es menor a tres sumo un numero a la escena actual
        else
        {
            Contador.C1 = 0;
            Contador.C2 = 0;
            Contador.cScene = 0;
            SceneManager.LoadScene("Menu_inicio");//en otro caso significa que la partida ha terminado, ponemos todos los contadores a 0 para una nueva partida y cargamos la escena de menu

        }
           
        
    }

}


