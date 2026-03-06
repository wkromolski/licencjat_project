using UnityEngine;

public class OnTriggerChaseOn : MonoBehaviour
{
    [SerializeField] private GameObject npc;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject trigger2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {

            if (gameObject.name == "Trigger1")
            {
                ActivateTrigger2();
            }
            else if (gameObject.name == "Trigger2")
            {
                ActivateNPC();
            }
        }
    }

    private void ActivateTrigger2()
    {
        if (trigger2 != null)
        {
            trigger2.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void ActivateNPC()
    {
        if (npc != null)
        {
            npc.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}