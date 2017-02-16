using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {

    // Use this for initialization


    float x;
    float y;
    Vector2 pos;

    public GameObject medKit;

    GameObject botiquin;

    int random;
    int contadorRandom;
    private int numeroBotiquines = 0; 

    public GameObject[] inventarioArmasp1 = new GameObject[3];
    public GameObject[] inventarioArmasp2 = new GameObject[3];
   
    void Start()
    {
		//PlayerController p1 = GetComponent<PlayerController> ();
        //oculto todas las armas que contiene el personaje
        for(int i = 0; i < inventarioArmasp1.Length; i++)
        {
            inventarioArmasp1[i].SetActive(false);
            inventarioArmasp2[i].SetActive(false);
        }



    }

    // Update is called once per frame
    void Update()
    {
        
        //si la variable random es mayor de 90 crea un botiquin
		if (random >= 90 && numeroBotiquines <= 2)
        {
            x = Random.Range(-9, 9);
            y = 6;
            pos = new Vector2(x, y);

            botiquin = (GameObject)Instantiate(medKit, pos, Quaternion.identity);

            //Se le asocia un tag para controlar si entra en colision con un botiquin.
            botiquin.tag = "medKit";
            numeroBotiquines++;

            getRandom();
        }

        //si no hago un aleatorio y si el resultado es  igual a 99 llamo a la funcion getrandom
        else if(random==0||random<=89)
        {
            if (Random.Range(1, 100) >=97 )
            {
                getRandom();
            }

        }

        



    }


    //private int getNumeroBotiquines()
    //{
    //    return numeroBotiquines;
    //}

    private void getRandom()
    {
         random = Random.Range(1, 100);//ejecuta un aleatorio  y lo guarda en la variable Random

    }



    

    
    


   
}
