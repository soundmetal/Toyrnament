using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

	public float explosion_delay = 0.25f;
	public float explosion_rate = 1f;
	public float explosion_max_size = 2.35f;
	public float explosion_speed = 10f;
	public float current_radius = 0f;

	bool exploded = false;
	CircleCollider2D explosion_radius;

	private bool start_delay = false;
	private bool explosionFlag = true;
	private bool explosionFlag2 = true;

	// Use this for initialization
	void Start () {
		explosion_radius = gameObject.GetComponent<CircleCollider2D> ();
	}

	void Update(){

        /*
        * Implementación de un delay a la explosión por si queremos hacer otro tipo de explosivos, como granadas.
        */

        if (start_delay == true) {
			explosion_delay -= Time.deltaTime;
			if (explosion_delay <= 0) {
				exploded = true;
			}
		}
	}

	void FixedUpdate(){

        /*
        * Comprueba si el objeto va a explotar.
        */

        if (exploded == true) 
		{
            //Aumenta el tamaño del radio de un trigger circular con un ratio de explosión.
            if (current_radius < explosion_max_size) 
			{
				current_radius += explosion_rate;
			} 
			else 
			{
                //Destruye el objeto una vez alcanzado el tamaño dado a la explosión.
                Object.Destroy (this.gameObject.transform.parent.gameObject);
			}

			explosion_radius.radius = current_radius;
		}
	}



    /*
   *   A cualquier objeto que entre en colisión con el trigger se le aplicará un AddForce, en la dirección opuesta por donde este a entrado en contacto.
   */
    void OnTriggerEnter2D(Collider2D col)
	{

		if (exploded == true) 
		{
			if (col.gameObject.GetComponent<Rigidbody2D>() != null) 
			{
				Vector2 target = col.gameObject.transform.position;
				Vector2 bomb = gameObject.transform.position;

				Vector2 direction = 800f * (target - bomb);

				col.gameObject.GetComponent<Rigidbody2D>().AddForce (new Vector2(direction.x * 1.5f, direction.y * 1f));

                //Comprueba si entra en colisión con los jugadores para restarles 2 upntos de vida

                if (col.gameObject.tag == "Player"  && explosionFlag) {
					col.gameObject.GetComponent<PlayerController> ().restarVida(2);
					explosionFlag = false;
				}
				if (col.gameObject.tag == "Player2" && explosionFlag2) {
					col.gameObject.GetComponent<PlayerController_2> ().restarVida2(2);
					explosionFlag2 = false;
				}
			}
		}
	}

    /*
   * La mina explota con la colisión de cualquier objeto mientras sea distinto del suelo.
   */

    void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag != "Ground") {
				start_delay = true;
		}
	}
}
