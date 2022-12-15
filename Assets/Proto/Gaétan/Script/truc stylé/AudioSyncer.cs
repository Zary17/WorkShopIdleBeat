using UnityEngine;

public class AudioSyncer : MonoBehaviour
{

    [SerializeField] protected float bias;
    [SerializeField] protected float timeStep;
    [SerializeField] protected float timeToBeat;
    [SerializeField] protected float restSmoothTime;

    private float m_previousAudioValue;
    private float m_audioValue;
    private float m_timer;

    protected bool m_isBeat;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate(); 
    }


    public virtual void OnBeat()
    {
        m_timer = 0;
        m_isBeat = true;
    }

    public virtual void OnUpdate()
    {
        m_previousAudioValue = m_audioValue;
        m_audioValue = AudioSpectrum.spectrumValue;

        if(m_previousAudioValue > bias && m_audioValue <= bias)
        {
            if(m_timer > timeStep)
            {
                OnBeat();
            }
        }

        if(m_previousAudioValue <= bias && m_audioValue > bias)
        {
            if(m_timer > timeStep)
            {
                OnBeat();
            }
        }


        m_timer += Time.deltaTime;
    }
}
