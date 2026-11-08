using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.Events;
public class AnimTestScript : MonoBehaviour
{
    [SerializeField] List<UnityEvent> actions = new List<UnityEvent>();
    [SerializeField] float durationBetweenActionChangeCall;

    int iterator = 0;
    public IEnumerator Start()
    {
        while (true)
        {
            actions[iterator].Invoke();
            iterator++;

            if(iterator >= actions.Count)
                iterator = 0;

            yield return new WaitForSeconds(durationBetweenActionChangeCall);
        }
    }

}
