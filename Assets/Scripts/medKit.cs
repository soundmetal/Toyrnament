using UnityEngine;
using System.Collections;

public class medKit : MonoBehaviour {


    GameObject player;

    // Use this for initialization
    void Start () {

        
        
	
	}
	
	// Update is called once per frame
	void Update () {





	
	}




    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            player = GameObject.Find("Player");
            coll.gameObject.GetComponent<PlayerController>().setLifeCount1();//llamo a funcion contenida en el script del player uno
            //numeroBotiquines--;
            Destroy(gameObject);
        }


        if(coll.gameObject.tag == "Player2")
        {
            player = GameObject.Find("Player2");
            coll.gameObject.GetComponent<PlayerController_2>().setLifeCount2();
            //numeroBotiquines--;
            Destroy(gameObject);

        }

		if (coll.gameObject.tag == "medKit") 
		{
			if (coll.gameObject.transform.position.y > transform.position.y) {
				Destroy (gameObject);//si el objeto colisionado tiene un transform mayor en y con el de este objeto se destruye este objeto
			}
		}


    }




}
