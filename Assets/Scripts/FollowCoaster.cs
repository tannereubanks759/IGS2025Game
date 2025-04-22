using UnityEngine;

public class FollowCoaster : MonoBehaviour
{
    public GameObject head;
    

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = head.transform.position;
        this.gameObject.transform.rotation = head.transform.rotation;
    }
}
