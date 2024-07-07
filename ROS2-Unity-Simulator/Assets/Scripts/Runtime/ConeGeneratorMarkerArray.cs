using RosMessageTypes.Visualization;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using UnityEngine;

public class ConeGeneratorMarkerArray : MonoBehaviour
{
    [SerializeField] private GameObject _blueConePrefab;
    [SerializeField] private GameObject _yellowConePrefab;
    [SerializeField] private GameObject _whiteTestConePrefab;
    private MarkerMsg[] _markers;

    public void GenerateConesFromMarkerArray(MarkerArrayMsg arrMsg)
    {
        _markers = arrMsg.markers;

        foreach(MarkerMsg marker in _markers)
        {
            Vector3 markerPos = new Vector3( (float)marker.pose.position.x,
                                              (float)marker.pose.position.y, 
                                              (float)marker.pose.position.z);
            ICoordinateSpace coordinateSpace = new FRD();
            Vector3 toUnitySpaceConvertedMarkerPos = coordinateSpace.ConvertToRUF(markerPos);

            //Instantiate(_blueConePrefab, toUnitySpaceConvertedMarkerPos, Quaternion.identity, this.transform);
            Instantiate(_blueConePrefab, toUnitySpaceConvertedMarkerPos, Quaternion.identity, this.transform.GetChild(1));
        }
    }
}
