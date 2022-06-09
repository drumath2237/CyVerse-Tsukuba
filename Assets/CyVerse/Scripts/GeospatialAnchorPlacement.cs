using Google.XR.ARCoreExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace CyVerse
{
    public class GeospatialAnchorPlacement : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI logText = null;

        [SerializeField]
        private AREarthManager earthManager = null;

        [SerializeField]
        private ARAnchorManager arAnchorManager = null;

        [SerializeField]
        private GameObject anchorObject = null;

        private Transform anchorTransform = null;

        private void Update()
        {
            if (!logText)
            {
                return;
            }

            var pose = earthManager.CameraGeospatialPose;
            var earthState = earthManager.EarthState;

            logText.text = $"Tracking State:{earthState}\nlat,lon:{pose.Latitude}, {pose.Longitude}";
            if (anchorTransform)
            {
                logText.text += $"anchor pose:{anchorTransform.position}";
            }
        }

        public void PlaceAnchor()
        {
            if (!arAnchorManager)
            {
                return;
            }

            var anchor = arAnchorManager.AddAnchor(
                36.08219375,
                140.1133591,
                68.958-1.5,
                Quaternion.identity
            );

            if (!anchorObject)
            {
                return;
            }

            var obj = Instantiate(anchorObject, anchor.transform);
            anchorTransform = obj.transform;
        }
        
        public void ToggleAnchorObject()
        {
            anchorTransform.gameObject.SetActive(!anchorTransform.gameObject.activeSelf);
        }
    }
}