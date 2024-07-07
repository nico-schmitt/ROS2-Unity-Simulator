using System.Collections;
using System.Collections.Generic;
using RosMessageTypes.DriverlessMessages;
using RosMessageTypes.Visualization;
using Unity.Robotics.ROSTCPConnector.ROSGeometry;
using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [Header("Physics Car")]
    public GameObject physicsCarPrefab;

    [Header("Position Only Car")]
    public GameObject positionOnlyCarPrefab;


    public GameObject physicsCarGameObject { get; set; }
    public GameObject positionOnlyCarGameObject { get; set; }
    public Rigidbody rb { get; set; }

    ICoordinateSpace coordinateSpace;


    public void Awake() => coordinateSpace = new FRD();



    public void GiveControlsToPhysicsCar(ControlsMsg msg)
    {
        Vector3 throttle = new Vector3(msg.throttle,0,0);
        Vector3 throttleInUnitySpace = coordinateSpace.ConvertToRUF(throttle);

        Vector3 a = new Vector3(0,0,0);
        if(Mathf.Abs(msg.throttle) > 0.02)
            a = physicsCarGameObject.transform.forward * msg.throttle;
        MainThreadDispatcher.EnqueueFixedUpdate(() => rb.AddForce(a));
        //rb.velocity += a;
        float curYaw = physicsCarGameObject.transform.rotation.eulerAngles.y;
        float newYaw = curYaw + msg.steering;

        Quaternion tmp = new Quaternion();

        // Vector3 steering = new Vector3(0, newYaw, 0);
        // Vector3 steeringInUnitySpace = coordinateSpace.ConvertToRUF(steering);
        // tmp.eulerAngles = steeringInUnitySpace;

        tmp.eulerAngles = new Vector3(0, newYaw, 0);
        Quaternion tmpInUnitySpace = coordinateSpace.ConvertToRUF(tmp);

        MainThreadDispatcher.EnqueueFixedUpdate(() => rb.AddTorque(Vector3.up * msg.steering));

        //physicsCarGameObject.transform.rotation = tmp;
        Debug.Log($"{msg.throttle} | {msg.steering}");
    }


    public void UpdateOnlyVehiclePos(MarkerMsg msg)
    {
        Vector3 vehiclePos = new Vector3( (float)msg.pose.position.x,
                                            (float)msg.pose.position.y,
                                            (float)msg.pose.position.z);                         
        Vector3 vehiclePosInUnitySpace = coordinateSpace.ConvertToRUF(vehiclePos);
        positionOnlyCarGameObject.transform.position = vehiclePosInUnitySpace;

        // Quaternion vehicleRot = new Quaternion((float)msg.pose.orientation.x,
        //                                        (float)msg.pose.orientation.y,
        //                                        (float)msg.pose.orientation.z,
        //                                        (float)msg.pose.orientation.w);
        // Quaternion vehicleRotInUnityspace = coordinateSpace.ConvertToRUF(vehicleRot);
        // positionOnlyCarGameObject.transform.rotation = vehicleRotInUnityspace;                                             
    }
}
