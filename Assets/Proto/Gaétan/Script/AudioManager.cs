using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Timeline;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager Instance = null;

    [SerializeField] float timeEffect;


    AudioSource basic = null;
    bool changePitch = false;

    [System.Serializable]
    public class Sound
    {
        public string name;

        public AudioClip[] clip;
        public AudioMixerGroup group;

        [Range(0f, 1f)]
        public float volume;
        [Range(.1f, 3f)]
        public float pitch;

        public bool loop;

        [HideInInspector]
        public AudioSource source;
    }








    void Awake()
    {




        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);





        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = null;
            s.source.outputAudioMixerGroup = s.group;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

    }

    private void Start()
    {
        Play("Menu");

    }

    private void Update()
    {
        if (changePitch && basic.pitch>0)
        {

            //basic.pitch = Mathf.Lerp(basic.pitch, 0, 1f * Time.deltaTime);
            basic.pitch -= Time.deltaTime / timeEffect;
            
        }
        else
            changePitch = false;


    }


    public void Play(string name)
    {

      

        Sound p = System.Array.Find(sounds, sound => sound.name == name);
        p.source.clip = p.clip[Random.Range(0, p.clip.Length)];
        if (p == null)
        {
            Debug.LogWarning("Il n'y a pas de clip ayant " + name + "comme nom!");
            return;

        }
        p.source.Play();
    }


    public void Stop(string name)
    {



        Sound p = System.Array.Find(sounds, sound => sound.name == name);
        p.source.clip = p.clip[Random.Range(0, p.clip.Length)];
        if (p == null)
        {
            Debug.LogWarning("Il n'y a pas de clip ayant " + name + "comme nom!");
            return;

        }
        p.source.Stop();
    }







    public AudioSource CheckPointEffect(string name)
    {
        AudioSource audioSource;

        Sound p = System.Array.Find(sounds, sound => sound.name == name);
        if (p == null)
        {
            Debug.LogWarning("Il n'y a pas de clip ayant " + name + "comme nom!");
            return null;


        }

        audioSource = p.source;

        //float (Mathf.Lerp(p.pitch, 0,0));

        basic = audioSource;
        changePitch = true;

        return audioSource;
    }




    public IEnumerator StartFade(string name, float duration, float targetVolume)
    {
        Sound p = System.Array.Find(sounds, sound => sound.name == name);
        float currentTime = 0;
        float start = p.source.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            p.source.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }



    public void ChangeScene(string label, float duree, float volumeVisee)
    {
        StartCoroutine(StartFade(label, duree, volumeVisee));
    }



}
