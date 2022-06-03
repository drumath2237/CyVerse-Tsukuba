using System;
using Google.XR.ARCoreExtensions;
using TMPro;
using UnityEngine;

namespace GeospatialTsukuba
{
    public class GeospatialAnchorPlacement : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI logText;

        [SerializeField]
        private AREarthManager earthManager;

        

        private void Update()
        {
            if (!logText)
            {
                return;
            }

            var pose = earthManager.CameraGeospatialPose;
            var earthState = earthManager.EarthState;

            logText.text = $"Tracking State:{earthState}\nlat,lon:{pose.Latitude}, {pose.Longitude}";
        }
    }
}