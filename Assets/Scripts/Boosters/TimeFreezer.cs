using System.Collections;
using UnityEngine;

public class TimeFreezer : Booster, IClickable
{
    [SerializeField] private float timeToFreeze;
    
    public void Click()
    {
        StartCoroutine(Boost());
    }

    public override IEnumerator Boost()
    {
        storedPosition = transform.position;
        transform.position = new Vector3(storedPosition.x, -2, storedPosition.z);
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(timeToFreeze);
        Time.timeScale = 1;
        StartCoroutine(CoolDown());
    }
}
