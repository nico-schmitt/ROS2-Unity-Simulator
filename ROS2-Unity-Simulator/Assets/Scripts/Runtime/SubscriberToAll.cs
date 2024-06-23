using System;
using System.Collections;
using System.Collections.Generic;
using RosMessageTypes.BuiltinInterfaces;
using RosMessageTypes.DriverlessMessages;
using RosMessageTypes.Geometry;
using RosMessageTypes.Std;
using RosMessageTypes.Visualization;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class SubscriberToAll : MonoBehaviour
{
    private ROSConnection rosCon;

    [SerializeField] private ConeGeneratorMarkerArray _coneGenMarkArr;

    [SerializeField] private bool _simVizCones;
    [SerializeField] private bool _simVizPosition;

    [SerializeField] private GameObject _carPrefab;
    private GameObject _carGameObject;

    private bool _messageReceived = false;
    private bool _unsubscribePending = false;

    private int _curMarkerMsgCount = 0;
    

    private string _simConesTopic = "/sim_viz/cones";
    private string _simPositionTopic = "/sim_viz/position";

    public void Start()
    {
        rosCon = ROSConnection.GetOrCreateInstance();
        _carGameObject = Instantiate(_carPrefab, new Vector3(0,0,0), Quaternion.identity);
        SubscribeToWhatIsEnabled();
    }

    private void SubscribeToWhatIsEnabled()
    {
        Debug.Log("Subscribes Enabled");
        if(_simVizCones) rosCon.Subscribe<MarkerArrayMsg>(_simConesTopic, GenCones);
        if(_simVizPosition)  rosCon.Subscribe<MarkerMsg>(_simPositionTopic, UpdateVehiclePos);
        
    }

    private void UpdateVehiclePos(MarkerMsg msg)
    {
        Vector3 vehiclePos= new Vector3( (float)msg.pose.position.x,
                                                         (float)msg.pose.position.y,
                                                         (float)msg.pose.position.z);
        ICoordinateSpace coordinateSpace = new FRD();                                                         
        Vector3 toUnitySpaceConvertedVehiclePos = coordinateSpace.ConvertToRUF(vehiclePos);
        _carGameObject.transform.position = toUnitySpaceConvertedVehiclePos;
    }

    private void GenCones(MarkerArrayMsg msg)
    {

        _curMarkerMsgCount++;
        // Only every third message sends out complete track data (????), and the static track only has to be generated once
        if(_curMarkerMsgCount == 3)
        {
            _coneGenMarkArr.GenerateConesFromMarkerArray(msg);
            _unsubscribePending = true;
        }       
    }

    private void Update()
    {
        if(_unsubscribePending)
        {
            rosCon.Unsubscribe(_simConesTopic);
            _unsubscribePending = false;
        }
    }
}
