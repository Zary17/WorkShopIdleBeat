using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{

    [SerializeField] AudioSource _source;

    private float[] m_audioSpectrum = new float[128];

    public static float spectrumValue { get; private set; }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //AudioListener.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);

        _source.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);

        if(m_audioSpectrum !=null && m_audioSpectrum.Length > 0)
        {
            spectrumValue = m_audioSpectrum[0] * 100;
        }
    }
}
