using Google.XR.ARCoreExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace GeospatialTsukuba
{
    public class GeospatialAnchorPlacement : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI logText;

        [SerializeField]
        private AREarthManager earthManager;

        [SerializeField]
        private ARAnchorManager arAnchorManager;

        [SerializeField]
        private GameObject anchorObject = null;

        private Transform anchorTransform = null;

        [SerializeField]
        private GameObject testmodel;


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
                testmodel.transform.rotation = anchorTransform.rotation;
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
                68.958,
                Quaternion.identity
            );

            if (!anchorObject)
            {
                return;
            }

            var obj = Instantiate(anchorObject, anchor.transform);
            anchorTransform = obj.transform;
        }
    }
}