using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonSkil : MonoBehaviour
{
    [SerializeField]
    private GameObject shadowBall = null;

    [SerializeField]
    private float shotPower = 3;

    public void ChageShadowBall()
    {
        var vec = transform.parent.transform.position;
        var t = transform.parent;

        GameObject shaBall = Instantiate(shadowBall, vec, Quaternion.identity,transform.parent);

        shaBall.transform.position = vec + t.forward + t.up;
        StartCoroutine(ShotShadowBall(shaBall));
    }

    IEnumerator ShotShadowBall(GameObject g)
    {
        yield return new WaitForSeconds(1.5f);

        MusicOperation.instance.SetPlaySE("Shot");

        g.transform.parent = null;
        g.GetComponent<Rigidbody>().AddForce(transform.parent.forward * shotPower, ForceMode.Impulse);
        Destroy(g, 10);

        yield return null;
    }
}
