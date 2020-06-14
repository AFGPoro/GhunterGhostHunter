using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearAfterSeconds : MonoBehaviour
{
    public float secondsBeforeDestroyed;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(DisableAfterSeconds(secondsBeforeDestroyed));
    }

    IEnumerator DisableAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.gameObject.SetActive(false);
    }

}
