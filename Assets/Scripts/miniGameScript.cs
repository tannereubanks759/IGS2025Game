using Unity.VisualScripting;
using UnityEngine;

public class miniGameScript : MonoBehaviour
{
    public GameObject ui;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Entered zone");
            ui.SetActive(true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Left Zone");
            ui.SetActive(false);
        }
    }
}
