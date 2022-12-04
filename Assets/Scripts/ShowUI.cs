using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    public GameObject UIObject;
    // Start is called before the first frame update
    void Start()
    {
        UIObject.SetActive(false);
    }

    void OnTriggerEnter(Collider player) {
        if(player.gameObject.tag == "Player"){
            UIObject.SetActive(true);
            StartCoroutine("WaitForSec");
        }       
    }

    IEnumerator WaitForSec(){
        yield return new WaitForSeconds(4);
        Destroy(UIObject);
        Destroy(gameObject);
    }
}
