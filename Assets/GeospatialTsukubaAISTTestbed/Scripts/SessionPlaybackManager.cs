using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;

namespace GeospatialTsukuba
{
    public class SessionPlaybackManager : MonoBehaviour
    {
        [SerializeField]
        private ARSession arSession;

        [SerializeField]
        private bool playbackOnAwake;

        [SerializeField]
        private string mp4Filename = "arcore-session.mp4";

        private void Awake()
        {
            if (playbackOnAwake)
            {
                _ = PlaybackSessionOnAwakeAsync();
            }
        }

        private async Task PlaybackSessionOnAwakeAsync()
        {
            await Task.Delay(1000);
            PlaybackSession();
        }


        public void PlaybackSession()
        {
#if UNITY_ANDROID
            if (!arSession || arSession.subsystem is not ARCoreSessionSubsystem subsystem)
            {
                Debug.LogError("ar session is not compatible with ar core session");
                return;
            }

            if (subsystem.playbackStatus == ArPlaybackStatus.Finished)
            {
                StopPlayback();
            }


            var mp4Path = Path.Combine(Application.persistentDataPath, mp4Filename);
            if (!File.Exists(mp4Path))
            {
                Debug.LogError("mp4 file not found");
                return;
            }

            subsystem.StartPlayback(mp4Path);
#endif
        }

        public void StopPlayback()
        {
#if UNITY_ANDROID
            if (!arSession || arSession.subsystem is not ARCoreSessionSubsystem subsystem)
            {
                Debug.LogError("ar session is not compatible with ar core session");
                return;
            }

            subsystem.StopPlayback();
#endif
        }
    }
}