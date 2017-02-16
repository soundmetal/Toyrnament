using UnityEngine;
using System.Collections;




public class knockBox : MonoBehaviour {

    private Animator animator;

    public bool knock;
    public int aux=0;

    Vector2 position;

    public GameObject bomb;
    public GameObject pistola;
    public GameObject escopeta;


    public Transform box;

    GameObject objetoInstaciado;

    

    // Use this for initialization
    void Start () {

        animator = GetComponent<Animator>();
        position = this.transform.position;

    }
	
	// Update is called once per frame
	void Update () {

        //hecho antes de meter la animacion, cuando se golpea la caja la variable knock se pone a true pasa y ejecuta el codigo
        if (knock && aux==0)
        {
            aux = 1;//el objeto ha sido golpeado y su transform ha cambiado se inicia este contador para que pase un tiempo hasta que la caja vuelva a su posicion inicial
        }
        //ponemos aux a uno para que pase al siguiente if

        if (aux > 0)
        {
            aux++;
        }

        //vamos incrementando aux cuando llega a 20 pasa al siguiente if

         if (aux == 20)
        {
            this.transform.position = position;//asino la posicion inicial a la caja
            aux = -1;//pongo aux a menos uno para que no siga ejecutando el contador
        }
	
	}


    void OnCollisionEnter2D(Collision2D coll)
    {
		if((coll.gameObject.tag == "Player"|| coll.gameObject.tag=="Player2") && knock==false)
        {
            //this.transform.Translate(0, +1, 0);//cambio transform para que bloque suba
            animator.SetBool("activar_caja", true);
            //Activa la animacion y pasa a un sprite donde la caja esta abierta.
            //En el futuro, haremos que sea una animación de donde salen confetis y cosas así al abrir la caja.

            knock = true;//variable golpe a true para poder entrar en las condiciones del update

            int random = Random.Range(0, 100);//hago random para que se instancio uno de los tres tipos de armas que existen en el juego

            if (random < 20)
            {
               objetoInstaciado=(GameObject) Instantiate(bomb, box.position, box.rotation);//instancio el item en el escenario
                
            }

            else if(random>=20 && random < 70)
            {
                objetoInstaciado = (GameObject)Instantiate(pistola, box.position, box.rotation);
                
            }

            else if(random>=70 && random <= 100)
            {
                objetoInstaciado = (GameObject)Instantiate(escopeta, box.position, box.rotation);
                
            }
            
        }

       




    }





}
