using UnityEngine;

public class VerySmallJumpscare : MonoBehaviour
{
    public GameObject pointLight;
    public GameObject glowl;
    public GameObject light2;
    public AudioSource audios;
    public GameObject closed;
    public float timeToDestroy = 7f;

    void Start()
    {
        pointLight.SetActive(true);
        audios.Stop();
        glowl.SetActive(false);
        light2.SetActive(true);
        closed.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pointLight.SetActive(false);
            audios.Play();
            glowl.SetActive(true);
            light2.SetActive(false);
            Destroy(gameObject, timeToDestroy);
            closed.SetActive(false);
        }
    }
}
