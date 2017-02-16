using UnityEngine;
using System.Collections;

public class shootDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject);//destruimos el objeto que sale de este collider usado para destruir las balas que se pierden a lo ancho de la escena
    }






}
