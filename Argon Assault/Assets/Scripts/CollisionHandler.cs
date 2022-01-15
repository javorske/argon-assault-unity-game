using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem crashExplosion;

    private MeshRenderer [] shipPartsMeshes;

    private void Start()
    {
        shipPartsMeshes = GetComponentsInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCrachSequence();
    }

    private void StartCrachSequence()
    {
        GetComponent<PlayerControls>().enabled = false;
        crashExplosion.Play();
        GetComponent<BoxCollider>().enabled = false;
        foreach (MeshRenderer item in shipPartsMeshes)
        {
            item.enabled = false;
        }
        StartCoroutine(ReloadLevel());
    }
    private IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(1f); 
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
