using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Chase : MonoBehaviour
{
    public NavMeshAgent ai;
    public List<Transform> destinations;
    public Animator aiAnim;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, idleTime, sightDistance, catchDistance, chaseTime, minChaseTime, maxChaseTime, jumpscareTime;
    public bool walking, chasing;
    public Transform player;
    Transform currentDest;
    Vector3 dest;
    int randNum;
    public int destinationAmount;
    public Vector3 rayCastOffset;
    public string deathScene;

    [SerializeField] private DissolvingControllerNonCoroutine dissolvingController;
    [SerializeField] private float startDissolvingDelay = 0f;

    [SerializeField] private AudioSource chaseSound;
    [SerializeField] private AudioSource footstepsSound;

    [SerializeField] private float fadeSpeed = 0.5f; // Jak szybko ma si? wycisza? (wi?ksza liczba = szybciej)
    [SerializeField] private float fadeFootstepsSpeed = 0.1f; // Jak szybko ma si? wycisza? (wi?ksza liczba = szybciej)

    void Start()
    {
        walking = true;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dissolvingController.Dissolve();
        }


        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + rayCastOffset, direction, out hit, sightDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                walking = false;
                StopCoroutine("stayIdle");
                StopCoroutine("chaseRoutine");
                StartCoroutine("chaseRoutine");
                chasing = true;
            }
        }
        if (chasing == true)
        {
            dest = player.position;
            ai.destination = dest;
            ai.speed = chaseSpeed;
            //aiAnim.ResetTrigger("walk");
            if (!chaseSound.isPlaying)
            {
                chaseSound.Play();
                footstepsSound.Play();
            }


            //if (chaseSound.isPlaying && dissolvingController.isDissolving == false)
            //{
               
            //    chaseSound.Stop();
            //}


            //aiAnim.SetTrigger("sprint");
            float distance = Vector3.Distance(player.position, ai.transform.position);
            if (distance <= catchDistance)
            {
               // dissolvingController.Dissolve();
                Debug.Log("Z?APANY!!!");
                //player.gameObject.SetActive(false);
                //aiAnim.ResetTrigger("walk");

                //aiAnim.ResetTrigger("sprint");
                //aiAnim.SetTrigger("jumpscare");
                //StartCoroutine(deathRoutine());
                //chasing = false;


                chaseSound.volume -= fadeSpeed * Time.deltaTime;
                footstepsSound.volume -= fadeFootstepsSpeed * Time.deltaTime;

                if (chaseSound.volume <= 0 && footstepsSound.volume <= 0)
                {
                    chaseSound.Stop();
                    footstepsSound.Stop();
                    gameObject.SetActive(false);
                }


            }
        }
        if (walking == true)
        {
            dest = currentDest.position;
            ai.destination = dest;
            ai.speed = walkSpeed;
            //aiAnim.ResetTrigger("sprint");

            aiAnim.SetTrigger("walk");
            if (ai.remainingDistance <= ai.stoppingDistance)
            {
                //aiAnim.ResetTrigger("sprint");
                //aiAnim.ResetTrigger("walk");

                ai.speed = 0;
                //StopCoroutine("stayIdle");
                //StartCoroutine("stayIdle");
                walking = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Sprawdzamy, czy obiekt, z którym si? zderzyli?my, ma tag "Player"
        if (other.CompareTag("Player"))
        {
            dissolvingController.Dissolve();
        }
    }

    IEnumerator stayIdle()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        walking = true;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }
    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        walking = true;
        chasing = false;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
    }
    IEnumerator deathRoutine()
    {
        yield return new WaitForSeconds(jumpscareTime);
        SceneManager.LoadScene(deathScene);
    }

    private IEnumerator StartDissolving(float dissolvingdelay)
    {

        yield return new WaitForSeconds(dissolvingdelay);



        dissolvingController.Dissolve();


    }
}
