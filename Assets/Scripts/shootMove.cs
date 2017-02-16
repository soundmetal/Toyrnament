using UnityEngine;
using System.Collections;

public class shootMove : MonoBehaviour {



    Rigidbody2D rb;
    public float speed;

    //GameObject player;
    public bool dispIzq;

    private Vector2 transBalaDestruida;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody2D>();
        //dispIzq = player.GetComponent<PlayerController>().getMirarDerecha();
        

     
    }
	
	// Update is called once per frame
	void Update () {

        //dispIzq = player.GetComponent<PlayerController>().getMirarDerecha();


        if (!dispIzq)
            rb.velocity = transform.right * speed;//si el personaje mira a la derecha nuestra bala disparada se mueve a la derecha

        else if(dispIzq)
            rb.velocity = (transform.right * -1) * speed;//en caso contrario la bala se dispara a la izquierda

        
    }



    public void voltearDisparo()
    {

        dispIzq = !dispIzq;//cada vez que esta funcion es llamada comprobamos en que estado se encuentra la variable booleana y la cambiamos
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
		
            Destroy(gameObject);//si la bala colisiona con otro objeto se destruye
        

    }




}
