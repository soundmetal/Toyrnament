using UnityEngine;
using System.Collections;

public class item : MonoBehaviour {


    public float fuerzaDisparo;
    public bool itemReposo;
    private Rigidbody2D rb;

    GameObject player;


	// Use this for initialization
	void Awake () {

        //rb.GetComponent<Rigidbody2D>();

        rb = GetComponent<Rigidbody2D>();
       
    }
	
	// Update is called once per frame
	void Update () {


        if (!itemReposo)
        {
            int random = Random.Range(0, 20);
            //random para hacer que objeto salga a izquierda o derecha
            if (random < 10)
                transform.Translate(-0.5f, +0, 5, 0);
            else
                transform.Translate(+0.5f, +0, 5, 0);

            itemReposo = true;
        }



	
	}



    void OnCollisionEnter2D(Collision2D coll)
    {
		if (coll.gameObject.tag == "Player")
        {

            player = GameObject.Find("Player");//busca gameobject player
            player.GetComponent<PlayerController>().armado=true;//obtengo el scrip del pbjeto player y cambio una de sus variables publicas a true para q en ese script ya nos permita disparar
            Destroy(gameObject);//destrullo este item

           


        }

        else if (coll.gameObject.tag == "Player2")
        {
            player = GameObject.Find("Player2");//busca gameobject player
            player.GetComponent<PlayerController_2>().armado2 = true;//obtengo el scrip del pbjeto player y cambio una de sus variables publicas a true para q en ese script ya nos permita disparar
            Destroy(gameObject);

        }


    }





}
