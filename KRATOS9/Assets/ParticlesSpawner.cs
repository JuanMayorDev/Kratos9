using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject hit_part;
    GameObject[] hit_parts = new GameObject[5];
    byte counter;
    #region Singleton

    /// <summary>
    /// Campo privado que referencia a esta instancia
    /// </summary>
    static ParticlesSpawner instance;

    /// <summary>
    /// Propiedad pública que devuelve una referencia a esta instancia
    /// Pertenece a la clase, no a esta instancia
    /// Proporciona un punto de acceso global a esta instancia
    /// </summary>
    public static ParticlesSpawner Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        ///Asigna esta instancia al campo instance
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);  ///Garantiza que sólo haya una instancia de esta clase   

    }

    #endregion    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < hit_parts.Length; i++)
        {

            GameObject o = Instantiate(hit_part, transform);
            hit_parts[i] = o ;
        }
    }

    public void SpawnHit(Vector3 pos)
    {
        hit_parts[counter].transform.position = pos;
        hit_parts[counter].GetComponent<ParticleSystem>().Play();
        StartCoroutine(ReturnToPool(hit_parts[counter]));
        if(counter < hit_parts.Length)
        {
            counter++;
        }
        else { counter = 0; }

    }
    IEnumerator ReturnToPool(GameObject o)
    {
        yield return new WaitForSeconds(2f);
        o.transform.position = transform.position;
    }
}
