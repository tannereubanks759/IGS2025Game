using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    public GameObject credits;
    public float scrollSpeed;

    private bool isWon;

    // Update is called once per frame
    void Update()
    {
       credits.transform.position = new Vector3(credits.transform.position.x, credits.transform.position.y + scrollSpeed * Time.deltaTime, 0);
    }
}
