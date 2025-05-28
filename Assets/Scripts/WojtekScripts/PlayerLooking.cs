using System.Collections;
using UnityEngine;

public class PlayerLooking : MonoBehaviour
{
    [SerializeField] private float disableCharacterDelay = 5f;
    [SerializeField] private float startDissolving = 3f;

    [SerializeField] private AudioSource jumpScareAudio;
    private bool isLooking;
    [SerializeField] float lookingRayLength = 10f;
    [SerializeField] private DissolvingController dissolvingController;

    void Update()
    {
        InteractRaycast();
    }

    void InteractRaycast()
    {
        Vector3 playerPosition = transform.position;
        Vector3 forwardDirection = transform.forward;

        Ray interactionRay = new Ray(playerPosition, forwardDirection);
        RaycastHit interactionRayHit;
        

        Debug.DrawLine(playerPosition, playerPosition + (forwardDirection * lookingRayLength));

        bool hitFound = Physics.Raycast(interactionRay, out interactionRayHit, lookingRayLength);
        

        if (hitFound)
        {
            GameObject hitGameObject = interactionRayHit.transform.gameObject;


            if (hitGameObject.CompareTag("npc") && !jumpScareAudio.isPlaying)

            {
                isLooking = hitGameObject.CompareTag("npc");
                Debug.Log("Trafiono NPC: " + hitGameObject.name + ". Wy³¹czam obiekt za " + disableCharacterDelay + " sekund.");
                StartCoroutine(DisableObjectAfterDelay(hitGameObject, disableCharacterDelay));
                StartCoroutine(StartDissolving(startDissolving));
               jumpScareAudio.PlayOneShot(jumpScareAudio.clip, 0.3f);


            }
            else
            {
                //Debug.Log("Trafiono: " + hitGameObject.name);
            }
        }
        else
        {
           // Debug.Log("-");
        }
    }

    private IEnumerator DisableObjectAfterDelay(GameObject objectToDisable, float delay)
    {

        yield return new WaitForSeconds(delay);


        if (objectToDisable != null)
        {

            objectToDisable.SetActive(false);
            Debug.Log(objectToDisable.name + " zosta³ wy³¹czony po " + delay + " sekundach.");

        }
    }

    private IEnumerator StartDissolving(float dissolvingdelay)
    {

        yield return new WaitForSeconds(dissolvingdelay);


       
            dissolvingController.Dissolve();

       
    }

    public bool IsLooking()
    {
        return isLooking;
    }
}
