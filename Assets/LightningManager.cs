using System.Collections;
using UnityEngine;

public class LightningManager : MonoBehaviour
{
    public void Update()
    {
        StartCastLightning();
    }

    public void StartCastLightning()
    {
        StartCoroutine(CastLightningCoroutine());
    }

    private IEnumerator CastLightningCoroutine()
    {
        foreach (var cable in GetComponentsInChildren<Cable>())
        {
            cable.OpenCable();
            yield return new WaitForSeconds(1.2f);
            cable.CloseCable();
        }

        yield return null;
    }
}
