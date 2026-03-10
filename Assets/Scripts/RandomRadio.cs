using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRadio : MonoBehaviour
{
    [Header("Radio News Objects")]
    [SerializeField] private List<GameObject> radioNews;

    private static int lastIndex = -1;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || triggered)
            return;

        triggered = true;

        int index = GetRandomNews();

        if (radioNews[index] != null)
            radioNews[index].SetActive(true);

        gameObject.SetActive(false);
    }

    private int GetRandomNews()
    {
        int index;

        do
        {
            index = Random.Range(0, radioNews.Count);
        }
        while (index == lastIndex && radioNews.Count > 1);

        lastIndex = index;

        return index;
    }
}