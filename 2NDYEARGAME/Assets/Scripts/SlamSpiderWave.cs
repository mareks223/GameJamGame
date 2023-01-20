using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamSpiderWave : MonoBehaviour
{
    public Collider WaveColliderOne;
    public Collider WaveColliderTwo;
    
    
    void Start()
    {
        Destroy(gameObject, 3f);
        WaveColliderOne.enabled = true;
        WaveColliderTwo.enabled = true;
        StartCoroutine(DiseableColliders());
    }
    IEnumerator DiseableColliders()
    {
        yield return new WaitForSeconds(1);
        WaveColliderOne.enabled = false;
        WaveColliderTwo.enabled = false;
     
    }
}
