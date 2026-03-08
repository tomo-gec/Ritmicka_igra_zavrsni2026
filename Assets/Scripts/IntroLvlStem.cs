using UnityEngine;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    public AudioSource kickSource;
    public AudioSource snareSource;

    [Range(0.01f, 1.0f)]
    public float threshold = 0.05f;
    public float cooldownTime = 0.5f;
    public int bpm;

    void Start()
    {
        if (kickSource != null && kickSource.clip != null)
            ScanAmpPeaks(kickSource.clip, "KICK");
        if (snareSource != null && snareSource.clip != null)
            ScanAmpPeaks(snareSource.clip, "SNARE");

        kickSource.Play();
        snareSource.Play();
        bpm = UniBpmAnalyzer.AnalyzeBpm(kickSource.clip);
    }

    void ScanAmpPeaks(AudioClip clip, string label)
    {

        float[] allSamples = new float[clip.samples * clip.channels];
        clip.GetData(allSamples, 0);

        int sampleRate = clip.frequency;
        int channels = clip.channels;


        int skipSamples = (int)(cooldownTime * sampleRate * channels);

        Debug.Log($"--- Pre-Scanning {label} ---");


        for (int i = 0; i < allSamples.Length; i += channels)
        {

            if (Mathf.Abs(allSamples[i]) > threshold)
            {
                float timeInSeconds = (float)i / (sampleRate * channels);
                Debug.Log($"<color=orange><b>[PRE-SCAN] {label}</b></color> found at: <b>{timeInSeconds:F2}s</b>");

                i += skipSamples;
            }
        }
    }
}