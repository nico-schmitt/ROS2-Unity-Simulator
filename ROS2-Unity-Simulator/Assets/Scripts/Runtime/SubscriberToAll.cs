using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
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

[RequireComponent(typeof(CarHandler))]
public class SubscriberToAll : MonoBehaviour
{
    private ROSConnection rosCon;

    [SerializeField] private ConeGeneratorMarkerArray _coneGenMarkArr;

    [SerializeField] private bool _simVizCones;
    [SerializeField] private bool _simVizPosition;

    [SerializeField] public bool _showPhysicsCar;
    [SerializeField] public bool _showPositionOnlyCar;

    private CarHandler _carHandler;


    private bool _messageReceived = false;
    private bool _unsubscribePending = false;

    private int _curMarkerMsgCount = 0;
    

    private const string SimConesTopic = "/sim_viz/cones";
    private const string SimPositionTopic = "/sim_viz/position";
    private const string SimControlsTopic = "/should_controls";



    public void Awake()
    {
        rosCon = ROSConnection.GetOrCreateInstance();
        _carHandler = this.GetComponent<CarHandler>();
        SubscribeToWhatIsEnabled();
    }

    private void SubscribeToWhatIsEnabled()
    {
        Debug.Log("Subscribes Enabled");
        if(_simVizCones) rosCon.Subscribe<MarkerArrayMsg>(SimConesTopic, GenCones);
        if(_showPhysicsCar)
        {
            _carHandler.physicsCarGameObject = Instantiate(_carHandler.physicsCarPrefab, new Vector3(0,0,0), Quaternion.identity);
            _carHandler.rb = _carHandler.physicsCarGameObject.GetComponent<Rigidbody>();
            rosCon.Subscribe<ControlsMsg>(SimControlsTopic, _carHandler.GiveControlsToPhysicsCar);
        }
        if(_showPositionOnlyCar)
        {
            _carHandler.positionOnlyCarGameObject = Instantiate(_carHandler.positionOnlyCarPrefab, new Vector3(0,0,0), Quaternion.identity);
            rosCon.Subscribe<MarkerMsg>(SimPositionTopic, _carHandler.UpdateOnlyVehiclePos);
        }
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
            rosCon.Unsubscribe(SimConesTopic);
            _unsubscribePending = false;
        }
    }
}
