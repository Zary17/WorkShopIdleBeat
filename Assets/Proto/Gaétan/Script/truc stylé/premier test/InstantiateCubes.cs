using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCubes : MonoBehaviour
{
    public GameObject _prefabCube;
    GameObject[] _sampleCube = new GameObject[512];
    public float maxScale;
    public GetAudioSpectrum peer;

    //Bon ca flemme de ranger c'est juste le truc pour instantier les cubes
    //juste tu référencie le audioSpectrum, un prefab de cube et le maxScale sert à mieux différencier les 
    //pics

    // Start is called before the first frame update
    void Start()
    {
        float x = transform.position.x;
        for (int i = 0; i < 30; i++)
        {
            GameObject _instance = (GameObject)Instantiate(_prefabCube);
            _instance.transform.position = this.transform.position;
            _instance.transform.parent = this.transform;
            _instance.name = "SampleCube" + i;
            //this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            _instance.transform.position = new Vector3(x, transform.position.y, transform.position.y);
            x += 1;
            _sampleCube[i] = _instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 512; i++)
        {
            if (_sampleCube != null)
            {
                _sampleCube[i].transform.localScale = new Vector3(0.5f, (peer._samples[i] * maxScale) + 0.5f, 0.5f);

                if (_sampleCube[i].transform.localScale.y >= 6)
                {
                    _sampleCube[i].GetComponent<SpriteRenderer>().color = Color.red;
                }
                else if(_sampleCube[i].transform.localScale.y >= 1)
                {
                    _sampleCube[i].GetComponent<SpriteRenderer>().color = Color.yellow;
                }
                else
                {
                    _sampleCube[i].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
    }
}