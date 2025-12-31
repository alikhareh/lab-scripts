using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckDeviceAnalyzeEvent : MonoBehaviour
{
    public List<DialogueNode> wrongWaveLenDialogues, correctWaveLenDialogues, finalDialogues;
    public SpectoAssistBrainManager assistBrainManager;
    public SpectroDeviceManager deviceManager;
    public void Check()
    {
        if (deviceManager.sample != null && deviceManager.sample.TryGetComponent<SpectroSampleManager>(out var sampleManager))
        {
            
            if (deviceManager.sourceWaveLenght == sampleManager.bestWaveLenght)
            {
                finalDialogues.AddRange(wrongWaveLenDialogues);
            }
            else
            {
                finalDialogues.AddRange(correctWaveLenDialogues);
            }

            assistBrainManager.DialogueNodes = finalDialogues;
        }
    }
}