using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnihilationSlam : MonoBehaviour
{
    public Collider waveCollider;
    
    
    
    void Start()
    {
        Destroy(gameObject, 6f);
        
        StartCoroutine(EnableColliders());
    }
    IEnumerator EnableColliders()
    {
        yield return new WaitForSeconds(4.5f);
        waveCollider.enabled = true;
        StartCoroutine(DiseableColliders());
    }
    IEnumerator DiseableColliders()
    {
        yield return new WaitForSeconds(0.1f);
        waveCollider.enabled = false;
    }
}
