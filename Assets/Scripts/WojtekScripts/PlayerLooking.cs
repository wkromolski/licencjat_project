using System.Collections;
using UnityEngine;

public class PlayerLooking : MonoBehaviour
{
    [SerializeField] private float disableCharacterDelay = 5f;
    [SerializeField] private float startDissolvingDelay = 3f;

    [SerializeField] private AudioSource jumpScareAudio;
    private bool isLooking;
    [SerializeField] float lookingRayLength = 10f;
    [SerializeField] private DissolvingController dissolvingController;
    [SerializeField] private DissolvingController dissolvingControllerCharWalking;



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
                Debug.Log("Hit NPC: " + hitGameObject.name + ". Dissolving character in " + startDissolvingDelay + " seconds.");
               // StartCoroutine(DisableObjectAfterDelay(hitGameObject, disableCharacterDelay));
                StartCoroutine(StartDissolving(startDissolvingDelay));
                jumpScareAudio.PlayOneShot(jumpScareAudio.clip, 0.3f);
                dissolvingController.TurnOffCollider();


            }
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
