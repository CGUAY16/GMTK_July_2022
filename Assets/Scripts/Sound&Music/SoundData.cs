using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is a script that is used to store all sound data files for easy grabbing.
// add this to an emtpy G.O and reference that G.O in the script you want sound/music done.
// Also, make sure that there are no files with the same enum value in their enum entry.
public class SoundData : MonoBehaviour
{
    public SoundFXContainer[] soundFiles;
    public MusicContainer[] musicFiles;
}
