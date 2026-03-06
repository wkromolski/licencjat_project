using UnityEngine;

public class OnTriggerChaseOn : MonoBehaviour
{
    [SerializeField] private GameObject npc;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject trigger2;

    private void OnTriggerEnter(Collider other)
    {
        // 1. Najpierw sprawdzamy, czy to na pewno Gracz dotkn?? triggera
        if (other.transform == player)
        {
            // 2. Sprawdzamy nazw? OBIEKTU, NA KTÓRYM JEST TEN SKRYPT (this.name)
            // To pozwoli nam rozró?ni?, w któr? kostk? w?a?nie wszed? gracz
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

    // Metody wyci?gni?te na zewn?trz dla porz?dku
    private void ActivateTrigger2()
    {
        if (trigger2 != null)
        {
            trigger2.SetActive(true);
            Debug.Log("Trigger 1 zaliczony -> Aktywowano Trigger 2");
            // Opcjonalnie: wy??cz Trigger 1, ?eby nie odpala? si? dwa razy
            gameObject.SetActive(false);
        }
    }

    private void ActivateNPC()
    {
        if (npc != null)
        {
            npc.SetActive(true);
            Debug.Log("Trigger 2 zaliczony -> NPC si? pojawia!");
            gameObject.SetActive(false);
        }
    }
}