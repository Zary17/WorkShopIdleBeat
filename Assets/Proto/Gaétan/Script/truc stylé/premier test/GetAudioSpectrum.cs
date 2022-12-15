using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAudioSpectrum : MonoBehaviour
{
    [SerializeField] AudioSource _source;
    public float[] _samples = new float[512];

    //T'occupes pas trop de ces variables elles sont importantes que dans le code
    bool beat;
    float beatTime;
    float maxRate;
    bool beginBeat;
    float t;
    float moyenneVal;
    float nbrFreq;
    float val;
    float[] arrayMoyenne = new float[30];
    int arrayIndex;

    [Header("Variable BPM")]
    [SerializeField] float bpm;

    [Header("Variables beat")]
    [SerializeField] float maxTime;
    [SerializeField] float treshold;
    [SerializeField] int primarySample;

    [Header("Variables m�lodie")]
    [SerializeField] float modificateur;
    [SerializeField] float PStreshold;
    [SerializeField] Vector2 forkPSamples;

    //Ici on initialise la source qui est sur le gameObject, le bpm qu'il faudra que tu retrouves avec le site que
    //je t'ai donn� et le nombre de fr�quences dans la fourchette de fr�quences (si tu en as besoin).
    void Start()
    {
        //_source = GetComponent<AudioSource>();

        maxRate = 60 / bpm;

        nbrFreq = (int)forkPSamples.y - forkPSamples.x;
    }

    void Update()
    {
        GetSpectrumAudioSource();

        //Ca c'est pour reset le beat au bout d'un moment (ici maxTime), si tu veux un beat 
        //qui reste plus longtemps pour x ou y raison, augmente maxTime. De base il est � 0.2 pour moi.
        if (beat)
        {
            t += Time.deltaTime;
            if (t >= maxTime)
            {
                t = 0;
                beat = false;
            }
        }

        //Quand le treshold est d�pass� pour la premi�re fois
        //ca lance le compteur en bpm, ca permet juste d'avoir le bpm
        //adapt� au premier beat d�tect�. Il appelle une fonction � chaque beat.
        if (beginBeat)
        {
            beatTime += Time.deltaTime;

            if (beatTime >= maxRate)
            {
                BPMBeatLight();
                beatTime -= maxRate;
            }
        }
    }

    //Ici le code r�cup�re la source et l'analyse.
    void GetSpectrumAudioSource()
    {
        //Ici il r�cup�re le spectre de la musique � chaque frame (n�cessaire pour conna�tre les 
        //valeurs en direct des fr�quences). Il les stocke dans un array d'une taille que tu d�finis en haut
        //(moi je met 512). 
        _source.GetSpectrumData(_samples, 0, FFTWindow.Blackman);

        //Ici il fait la moyenne des valeurs de forkPSamples
        MakeMoyenne();

        //La partie la plus importante, si la valeur du sample que tu as d�fini d�passe un seuil (ici threshold)
        // il execute le code dedans.
        if (_samples[primarySample] >= treshold)
        {
            beat = true;
            beginBeat = true;

            //Je laisse ces fonctions comme exemple tu peux les virer
            BeatLight();
            BeatParticule();
        }
    }

    void MakeMoyenne()
    {
        //Ici je r�cup�re toutes les valeurs des fr�quences en partant de forkPSamples.x jusqu'� forkPSamples.y
        for (int i = (int)forkPSamples.x; i < nbrFreq; i++)
        {
            moyenneVal += _samples[i];
        }

        CalculateTreshold();

        //Puis j'en fais une moyenne (ca c'est pas hyper opti je sais mais bon au moins ca marche)
        Melody((moyenneVal / nbrFreq) * 100);

        //ici je reset pour refaire une moyenne
        moyenneVal = 0;
    }

    void CalculateTreshold()
    {
        arrayMoyenne[(int)arrayIndex] = moyenneVal;

        if (arrayIndex < 10)
        {
            arrayIndex++;
        }
        else
        {
            arrayIndex = 0;
        }

        for (int i = 0; i < 30; i++)
        {
            val += arrayMoyenne[i];
        }

        PStreshold = val * modificateur;

        val = 0;
    }

    #region FonctionExemple

    //Je te laisse mes fonctions, j'ai utilis� des interfaces (IInteract) pour que ca soit plus simple
    //Libre � toi d'en faire ce que tu veux

    void Melody(float result)
    {
        //     if (result > PStreshold)
        //     {
        //         foreach (GameObject go in melodyObjectList)
        //         {
        //             go.GetComponent<IInteract>().Beat();
        //         }
        //     }
    }

    void BeatLight()
    {
        //     foreach (GameObject go in objectsList)
        //     {
        //         go.GetComponent<IInteract>().Beat();
        //     }
    }

    void BeatParticule()
    {
        //     foreach (GameObject go in melodyObjectList)
        //     {
        //         go.GetComponent<IInteract>().Beat2();
        //     }
    }

    void BPMBeatLight()
    {
        //     foreach (GameObject go in BPMobjectsList)
        //     {
        //         go.GetComponent<IInteract>().Beat();
        //     }
    }

    #endregion
}