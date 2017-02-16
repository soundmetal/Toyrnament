using UnityEngine;
using System.Collections;

public class PlayerController_2 : MonoBehaviour {

	private Rigidbody2D rb;
	private bool isOnGround = false;

	public static float speed=3.5f;

    public float jumpTime;
    private bool jumping = false;
    public Vector2 jumpVector;

    public Transform bazooka2;
    public Transform instanciadorRafaga2;

	public GameObject shoot2;
    public GameObject normalShoot2;
    public GameObject rafaga2;

	public bool armado2;
    public GameObject loser;

    GameObject bala;
    GameObject gameManager;

	enum DireccionDisparo { izquierda, derecha }
	DireccionDisparo direccionDisparo;

	BoxCollider2D playerCollider2;

    public GameObject[] life = new GameObject[3];
    int lifeCount = 2;


    bool escop;
    bool pist;
    bool bazoo;

    // Use this for initialization
    void Start () {

		playerCollider2 = GetComponent<BoxCollider2D> ();

		direccionDisparo = DireccionDisparo.izquierda;
		rb = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void FixedUpdate () {

		float distance = playerCollider2.bounds.extents.y + 0.1f;
		Vector3 origin = playerCollider2.bounds.center;

		//Depuracion, debe comentarse cuando ya no sea necesario verse el rayo
		//Debug.DrawLine(origin, origin + (-transform.up * distance), Color.yellow);

		RaycastHit2D hit;
		hit = Physics2D.Raycast (origin, -transform.up, distance, LayerMask.GetMask ("Ground"));

		if (hit.collider != null && (hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.tag == "box_weapons" || hit.collider.gameObject.tag == "Player")) 
		{
			isOnGround = true;
		} else
		{
			isOnGround = false;
		}

        //Dispara y comprueba si el jugador esta armado.
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && armado2)
        {
            //disparo normal
            if (pist == true)
            {
                bala = (GameObject)Instantiate(normalShoot2, bazooka2.position, bazooka2.rotation);//instancia una bala normal contenida en la variable normal shoot2
                if (direccionDisparo == DireccionDisparo.izquierda)//comprueba la direccion de disparo si esta a la izquierda llama a la funcion voltear disparo contenida en shootmove
                    bala.GetComponent<shootMove>().voltearDisparo();
            }



            //disparo rafaga

            if (escop == true)
            {

                bala = (GameObject)Instantiate(rafaga2, instanciadorRafaga2.position, instanciadorRafaga2.rotation);//instancia el objeto de tipo bala rafaga

                if (direccionDisparo == DireccionDisparo.izquierda)
                    bala.GetComponent<shootMove>().voltearDisparo();

                StartCoroutine(C1());//llamamos a una coroutine contenida en este script para hacer desaparecer los disparos

            }


            //disparo lento

            if (bazoo == true)
            {
                bala = (GameObject)Instantiate(shoot2, bazooka2.position, bazooka2.rotation);//instancia la bala de tipo shoot2
                if (direccionDisparo == DireccionDisparo.izquierda)
                {
                    bala.GetComponent<shootMove>().voltearDisparo();
                    bala.gameObject.transform.localScale = new Vector3(-bala.gameObject.transform.localScale.x, -bala.gameObject.transform.localScale.y, -bala.gameObject.transform.localScale.z);
                    //si el disparo mira hacia la izquierda cambiamos la escala del cohete ya que no es simetrico para que mire tambien en la misma direccion
                    //transform.position = new Vector3((transform.position.x + 1), transform.position.y, transform.position.z);
                }

                

            }

        }


		//Mover personaje
		rb.velocity = new Vector2 (Input.GetAxis ("Horizontal2") * speed, rb.velocity.y);

		//Voltear personaje
		if (Input.GetAxis ("Horizontal2") > 0 && direccionDisparo == DireccionDisparo.izquierda)
			voltear ();
		else if (Input.GetAxis ("Horizontal2") < 0 && direccionDisparo == DireccionDisparo.derecha)
			voltear ();

		//Saltar.
		if (isOnGround && Input.GetAxis ("Jump2") > 0 && jumping == false) {
            jumping = true;
            StartCoroutine(JumpRoutine());
        }


        //comprueba la vida que le queda al personaje
        if (lifeCount < 0)
        {

           
            
            loser.gameObject.GetComponent<finalpartida>().perder2 = true;//cambia la variable bool perder
            loser.gameObject.GetComponent<finalpartida>().final();//llama a la funcion final del script final de partida para comprobar jugador ganador
            Destroy(gameObject);

        }

    }


    //desaparecer disparo rafaga
    IEnumerator C1()
    {
        yield return new WaitForSeconds(2);//espero 4 segundos y destruyo el disparo de rafaga para que sea de corto alcance
        Destroy(bala.gameObject);
    }


    //Volteo del jugador con el arma.
    void voltear(){
		transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);//transformamos la escala del personaje

		if (direccionDisparo == DireccionDisparo.derecha)//comprobamos hacia donde mira el personaje si mira a la derecha cambiamos a izquierda y viceversa
			direccionDisparo = DireccionDisparo.izquierda;
		else
			direccionDisparo = DireccionDisparo.derecha;
	}


    //Detectamos que el jugador este en contacto con el suelo y pueda saltar.
    void OnCollisionEnter2D(Collision2D coll)
	{
        //si te da un disparo normal te quita vida
        if (coll.gameObject.tag == "normalShoot")
        {

            life[lifeCount].SetActive(false);
            lifeCount--;

        }

        //si la bala con la que colisiona es un cohete le resta dos puntos de vida
        if (coll.gameObject.tag == "rocket")
        {
            for (int i = 0; i < 2; i++)
            {

                life[lifeCount].SetActive(false);
                lifeCount--;
            }

        }






        //activo el tipo de arma que lleva el personaje segun el tag de la municion que recibe
        if (coll.gameObject.tag == "muniBazooka")
        {
            gameManager = GameObject.Find("gameManager");//buscamos el objeto gameManager ya que vamos a tener que acceder a su script
            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    gameManager.gameObject.GetComponent<gameManager>().inventarioArmasp2[i].SetActive(true);//activo el objeto contenido en el array inventarioArmas del objeto gameManager si estoy en la posicion cero
                    bazoo = true;//pongo la variable bazooka a true para que pueda ejercer el disparo del bazooka
                }

                else
                {
                    gameManager.gameObject.GetComponent<gameManager>().inventarioArmasp2[i].SetActive(false);//desactivo los objetos de las posiciones restantes por si tubiera otro cogido de antes
                    pist = false;
                    escop = false;// pongo estas booleanas a false por si tubiera otra arma antes
                }



            }
        }

        if (coll.gameObject.tag == "muniPistola")
        {
            gameManager = GameObject.Find("gameManager");//misma operacion de antes pero cambiando posiciones

            for (int i = 0; i < 3; i++)
            {
                if (i == 1)
                {
                    gameManager.gameObject.GetComponent<gameManager>().inventarioArmasp2[i].SetActive(true);
                    pist = true;
                }

                else
                {
                    gameManager.gameObject.GetComponent<gameManager>().inventarioArmasp2[i].SetActive(false);
                    escop = false;
                    bazoo = false;
                }


            }
        }

        else if (coll.gameObject.tag == "muniEscopeta")
        {
            gameManager = GameObject.Find("gameManager");

            for (int i = 0; i < 3; i++)
            {
                if (i == 2)
                {
                    gameManager.gameObject.GetComponent<gameManager>().inventarioArmasp2[i].SetActive(true);
                    escop = true;
                }

                else
                {
                    gameManager.gameObject.GetComponent<gameManager>().inventarioArmasp2[i].SetActive(false);
                    pist = false;
                    bazoo = false;
                }


            }
        }


    }

    //Corrutina para dejar el salto lo mas preciso que ahora mismo podemos.
    IEnumerator JumpRoutine()
    {
        rb.velocity = Vector2.zero;
        float timer = 0;

        while (Input.GetAxis("Jump2") > 0 && timer < jumpTime)
        {
            //Calculate how far through the jump we are as a percentage
            //apply the full jump force on the first frame, then apply less force
            //each consecutive frame

            float proportionCompleted = timer / jumpTime;
            Vector2 thisFrameJumpVector = Vector2.Lerp(jumpVector, Vector2.zero, proportionCompleted);
            rb.AddForce(thisFrameJumpVector, ForceMode2D.Impulse);
            timer += Time.deltaTime;
            yield return null;
        }

        jumping = false;
    }



    //funcion que es llamada por el objeto botiquin cuando este objeto colisiona con un personaje.
    public void setLifeCount2()
    {
        if (lifeCount < 2)
        {
            lifeCount++;
            life[lifeCount].SetActive(true);

        }

    }

	public void restarVida2(int cantidad){

		for (int i = 0; i < cantidad && lifeCount >= 0; i++) {
			life [lifeCount].SetActive (false);
			lifeCount--;
		}
	}


}

