using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
   // Start is called before the first frame update
    private void OntriggerEnter2D(Collider2D collision){
     Debug.Log("Moneda");
    if(collision.gameObject.tag == "Player"){
        GameController.SumaMoneda();
        Destroy(gameObject);

    }

  }

}
