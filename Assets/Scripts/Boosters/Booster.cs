using System.Collections;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] private float cooldown;

    protected Vector3 storedPosition;
    private bool isOnField;

    private void Update()
    {
        OnUpdateFunction();
    }

    public virtual void OnUpdateFunction()
    {
        if (!isOnField)
            Spawn();
    }

    public virtual void Spawn()
    {
        isOnField = true;
    }

    public virtual IEnumerator Boost()
    {
        storedPosition = transform.position;
        transform.position = new Vector3(storedPosition.x, -2, storedPosition.z);
        yield return null;
        StartCoroutine(CoolDown());
    }

    public virtual IEnumerator CoolDown()
    {
        yield return new WaitForSecondsRealtime(cooldown);
        transform.position = storedPosition;
        isOnField = false;
    }
}
