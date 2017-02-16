using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;
	private bool isOnGround = false;

	public static float speed=3.5f;

    public float jumpTime;
    private bool jumping = false;
    public Vector2 jumpVector;

    public Transform bazooka;
    public Transform instanciadorRafaga;

    public GameObject shoot;
    public GameObject normalshoot;
    public GameObject rafaga;

    public bool armado;

    GameObject bala;

    GameObject gameManager;

    enum DireccionDisparo { izquierda, derecha }
    DireccionDisparo direccionDisparo;

	BoxCollider2D playerCollider;

    public GameObject[] life = new GameObject[3];
    int lifeCount = 2;

    bool escop;
    bool pist;
    bool bazoo;

    public GameObject loser;

    // Use this for initialization
    void Start () {

		playerCollider = GetComponent<BoxCollider2D> ();

        direccionDisparo = DireccionDisparo.derecha;
		rb = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void FixedUpdate () {

        /*
      *Controla el salto del jugador, detecta si debajo de el hay un objeto 
      *que contenga la máscara "Ground" para poder saltar. También comprueba la distancia a la que se encuentra de ese objeto.
      */

        float distance = playerCollider.bounds.extents.y + 0.1f;
		Vector3 origin = playerCollider.bounds.center;

		//Depuracion, debe comentarse cuando ya no sea necesario verse el rayo
		//Debug.DrawLine(origin, origin + (-transform.up * distance), Color.yellow);

		RaycastHit2D hit;
		hit = Physics2D.Raycast (origin, -transform.up, distance, LayerMask.GetMask ("Ground"));

        //Controla que el personaje solo pueda saltar sobre cajas de munición, sobre otro personaje o sobre el suelo.

        if (hit.collider != null && (hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.tag == "box_weapons" || hit.collider.gameObject.tag == "Player2")) 
		{
			isOnGround = true;
		} else
		{
			isOnGround = false;
		}
        //Dispara y comprueba si el jugador esta armado.
        if (Input.GetKeyDown(KeyCode.Space) && armado)
        {
            //disparo normal
            if (pist == true)
            {
                bala = (GameObject)Instantiate(normalshoot, bazooka.position, bazooka.rotation);//mismas funciones comentadas en playerController2
                if (direccionDisparo == DireccionDisparo.izquierda)
                    bala.GetComponent<shootMove>().voltearDisparo();
            }



            //disparo rafaga

            if (escop == true)
            {

                bala = (GameObject)Instantiate(rafaga, instanciadorRafaga.position, instanciadorRafaga.rotation);

                if (direccionDisparo == DireccionDisparo.izquierda)
                    bala.GetComponent<shootMove>().voltearDisparo();

                StartCoroutine(C1());

            }


            //disparo lento

            if (bazoo == true)
            {
                bala = (GameObject)Instantiate(shoot, bazooka.position, bazooka.rotation);
                if (direccionDisparo == DireccionDisparo.izquierda)
                {
                    bala.GetComponent<shootMove>().voltearDisparo();
                    bala.gameObject.transform.localScale = new Vector3(-bala.gameObject.transform.localScale.x, -bala.gameObject.transform.localScale.y, -bala.gameObject.transform.localScale.z);
                    //transform.position = new Vector3((transform.position.x + 1), transform.position.y, transform.position.z);
                }

               



            }

        }

        //Mover personaje.
        rb.velocity = new Vector2 (Input.GetAxis ("Horizontal") * speed, rb.velocity.y);

		//Voltear personaje.
		if (Input.GetAxis ("Horizontal") > 0 && direccionDisparo == DireccionDisparo.izquierda)
			voltear ();
		else if (Input.GetAxis ("Horizontal") < 0 && direccionDisparo == DireccionDisparo.derecha)
			voltear ();

		//Salto.
		if (isOnGround && Input.GetAxis ("Jump") > 0 && jumping==false) {
            jumping = true;
            StartCoroutine(JumpRoutine());
        }


        if (lifeCount < 0)
        {
           
            //loser = GameObject.Find("finMundo");
            loser.gameObject.GetComponent<finalpartida>().perder = true;//cambia la variable bool perder
            loser.gameObject.GetComponent<finalpartida>().final();//llama a la funcion final del script final de partida para comprobar jugador ganador
            Destroy(gameObject);
            lifeCount = 2;
            
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

		transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        if (direccionDisparo == DireccionDisparo.derecha)
            direccionDisparo = DireccionDisparo.izquierda;
        else
            direccionDisparo = DireccionDisparo.derecha;
    }


    //Detectamos que el jugador este en contacto con el suelo y pueda saltar.
	void OnCollisionEnter2D(Collision2D coll)
	{
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




        if (coll.gameObject.tag == "muniBazooka")
        {
            gameManager = GameObject.Find("gameManager");
            //activo el arma q ha recogido el personaje desactiv0o las otras por si acaso tubiera otra arma cogida con anterioridad
            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    gameManager.gameObject.GetComponent<gameManager>().inventarioArmasp1[i].SetActive(true);
                    bazoo = true;
                }

                else
                {
                    gameManager.gameObject.GetComponent<gameManager>().inventarioArmasp1[i].SetActive(false);
                    pist = false;
                    escop = false;
                }


            }





        }

        if (coll.gameObject.tag == "muniPistola")
        {
            gameManager = GameObject.Find("gameManager");

            for (int i = 0; i < 3; i++)
            {
                if (i == 1)
                {
                    gameManager.gameObject.GetComponent<gameManager>().inventarioArmasp1[i].SetActive(true);
                    pist = true;
                }

                else
                {
                    gameManager.gameObject.GetComponent<gameManager>().inventarioArmasp1[i].SetActive(false);
                    escop = false;
                    bazoo = false;
                }


            }

        }

        if (coll.gameObject.tag == "muniEscopeta")
        {
            gameManager = GameObject.Find("gameManager");

            for (int i = 0; i < 3; i++)
            {
                if (i == 2)
                {
                    gameManager.gameObject.GetComponent<gameManager>().inventarioArmasp1[i].SetActive(true);
                    escop = true;
                }

                else
                {
                    gameManager.gameObject.GetComponent<gameManager>().inventarioArmasp1[i].SetActive(false);
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

        while (Input.GetAxis("Jump") > 0 && timer < jumpTime)
        {
            //Calcula cuanto llevamos del salto como un porcentaje
            //aplica toda la fuerza dada al salto en el primer frame, 
            //después esta fuerza es reducida a cada frame.

            float proportionCompleted = timer / jumpTime;
            Vector2 thisFrameJumpVector = Vector2.Lerp(jumpVector, Vector2.zero, proportionCompleted);
            rb.AddForce(thisFrameJumpVector, ForceMode2D.Impulse);
            timer += Time.deltaTime;
            yield return null;
        }

        jumping = false;
    }


    //funcion que es llamada por el objeto botiquin cuando este objeto colisiona con un personaje.
    public void setLifeCount1()
    {
        if (lifeCount < 2)
        {
            lifeCount++;
            life[lifeCount].SetActive(true);

        }

    }

    /*
  *   Función que controla la cantidad de vida que queremos restar al jugador (ya sea en función del arma, un golpe con un objeto, etc.)
  */
    public void restarVida(int cantidad){
		for (int i = 0; i < cantidad && lifeCount >= 0; i++) {
			life [lifeCount].SetActive (false);
			lifeCount--;
		}
	}







}
