using UnityEngine;

public class KiddieCoaster : MonoBehaviour
{
    // reference to the animator
    public Animator coasterAnimator;

    // reference to the interact text
    public GameObject interactUI;

    // reference to the mini game handler
    public miniGameScript miniGameObject;

    // cost of the trap
    public int costPrice;

    // has the player paid for the trap
    public bool paid;

    // the amount of revolutions that the coaster
    // has gone through
    private int revolutionsCompleted;

    // an array containing all the particle systems
    // pertaining to the flamethrower PS's
    public ParticleSystem [] Flamethrowers;

    // mats to show if trap is able to be bought or not
    public GameObject onMat;
    public GameObject offMat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // deactivate the interactUI
        interactUI.SetActive(false);

        // the player has not bought the trap at the
        // start of the game
        paid = false;

        // the trap has not completed any revolutions
        revolutionsCompleted = 0;

        // get all PS's in children in the coaster
        //Flamethrowers = GetComponentsInChildren<ParticleSystem> ();

        // turn each PS off and clear the particles at the start
        // so that it is "off"
        foreach (ParticleSystem p in Flamethrowers)
        {
            p.Pause ();
            p.Clear ();
        }

        onMat.SetActive(true);
        offMat.SetActive(false);
    }

    // called in an animation event at the end of the animation
    public void resetAnimationBool()
    {
        // the coaster will make 5 revolutions before stopping
        if (revolutionsCompleted == 4)
        {
            // set the animator bool to false
            coasterAnimator.SetBool("isOn", false);

            // reset the revolutions
            revolutionsCompleted = 0;

            // turn each PS off and clear the particles at the start
            // so that it is "off"
            foreach (ParticleSystem p in Flamethrowers)
            {
                p.Pause (); 
                p.Clear ();
            }

            onMat.SetActive(true);
            offMat.SetActive(false);

            // reset the paid bool
            paid = false;
        }
        else
        {
            // increment the revolutions
            revolutionsCompleted++;
        }
    }

    // called in interact.cs
    public void startCoaster()
    {
        // the player has enough tickets and the trap is not on
        if (miniGameObject.tickets >= costPrice && paid == false)
        {
            GameObject.FindAnyObjectByType<Interact>().PlayPurchaseSound();
            // set bools to true to turn the trap on
            paid = true;
            coasterAnimator.SetBool("isOn", true);

            // turn the particle systems on
            foreach (ParticleSystem p in Flamethrowers)
            {
                p.Play ();
            }

            // update the tickets
            miniGameObject.tickets -= costPrice;
            miniGameObject.ticketText.text = miniGameObject.tickets.ToString();

            // turn the onButton off and the offButton on
            onMat.SetActive(false);
            offMat.SetActive(true);
        }
    }
}
