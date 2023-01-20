using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Destroy : MonoBehaviour
{
    public Collider WaveColliderOne;
    public Collider WaveColliderTwo;
    public Collider WaveColliderThree;
    public Collider WaveColliderFour;
    
    void Start()
    {
        Destroy(gameObject, 8f);
        WaveColliderOne.enabled = true;
        StartCoroutine(WaveTwo());
    }
    IEnumerator WaveTwo()
    {
        yield return new WaitForSeconds(1);
        WaveColliderOne.enabled = false;
        WaveColliderTwo.enabled = true;
        StartCoroutine(WaveThree());
    }
    IEnumerator WaveThree()
    {
        yield return new WaitForSeconds(1);
        WaveColliderTwo.enabled = false;
        WaveColliderThree.enabled = true;
        StartCoroutine(WaveFour());
    }
    IEnumerator WaveFour()
    {
        yield return new WaitForSeconds(1);
        WaveColliderThree.enabled = false;
        WaveColliderFour.enabled = true;
        StartCoroutine(DiseableWaveFour());
        
    }
     IEnumerator DiseableWaveFour()
    {
        yield return new WaitForSeconds(1);
        WaveColliderFour.enabled = false;
        
        
    }

    
}
