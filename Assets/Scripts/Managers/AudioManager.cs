using UnityEngine;
using UnityEngine.Audio;

namespace LemApperson_UIPortfolio
{
    public class AudioManager : MonoBehaviour
    {
        #region Parameters

        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioSource _sfxSoundSource, _ambient1SoundSource, _ambient2SoundSource;
        static private AudioSource _sfxSound, _ambient1Sound, _ambient2Sound;
        private AudioMixerGroup _masterGroup, _ambientGroup, _sfxGroup;
        private float _masterVolume, _ambientVolume, _sfxVolume;
        public static AudioManager Instance { get; private set; }

        #endregion

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            _sfxSound = _sfxSoundSource;
            _ambient1Sound = _ambient1SoundSource;
            _ambient2Sound = _ambient2SoundSource;

            // Set the volumes using the constants
            SetAmbientVolume(0.1f);
            SetMasterVolume(0.8f);
            SetSFXVolume(2f);

            // Get and log the current values of the volumes
            float ambientVolume = GetAmbientVolume();
            float masterVolume = GetMasterVolume();
            float sfxVolume = GetSFXVolume();

            PlayAmbient1Sound();
        }

        #region Play Sounds

        public void PlayAmbient1Sound()
        {
            _ambient1Sound.Play();
        }

        public void PlayAmbient2Sound()
        {
            _ambient2Sound.Play();
        }

        public static void PlaySFXSound()
        {
            _sfxSound.Play();
        }

        #endregion

        #region Get & Set Volumes

        // Function to set the volume of a specific exposed parameter
        public void SetVolume(string parameterName, float volume)
        {
            _audioMixer.SetFloat(parameterName, Mathf.Log10(volume) * 20);
        }

        // Function to get the volume of a specific exposed parameter
        public float GetVolume(string parameterName)
        {
            float value;
            _audioMixer.GetFloat(parameterName, out value);
            return Mathf.Pow(10, value / 20);
        }

        // Example functions using the constants
        public void SetAmbientVolume(float volume)
        {
            SetVolume(AudioMixerConstants.AmbientVolume, volume);
        }

        public void SetMasterVolume(float volume)
        {
            SetVolume(AudioMixerConstants.MasterVolume, volume);
        }

        public void SetSFXVolume(float volume)
        {
            SetVolume(AudioMixerConstants.SFXVolume, volume);
        }

        public float GetAmbientVolume()
        {
            return GetVolume(AudioMixerConstants.AmbientVolume);
        }

        public float GetMasterVolume()
        {
            return GetVolume(AudioMixerConstants.MasterVolume);
        }

        public float GetSFXVolume()
        {
            return GetVolume(AudioMixerConstants.SFXVolume);
        }

        #endregion
    }
}

public static class AudioMixerConstants
{
    public const string AmbientVolume = "AmbientVolume";
    public const string MasterVolume = "MasterVolume";
    public const string SFXVolume = "SFXVolume";
}