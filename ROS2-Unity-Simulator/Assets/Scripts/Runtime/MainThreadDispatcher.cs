using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using RosMessageTypes.BuiltinInterfaces;
using RosMessageTypes.DriverlessMessages;
using RosMessageTypes.Geometry;
using RosMessageTypes.Std;
using RosMessageTypes.Visualization;
using Unity.Robotics.ROSTCPConnector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;


public class MainThreadDispatcher : MonoBehaviour
{
    public static readonly ConcurrentQueue<Action> _executionQueueUpdate = new ConcurrentQueue<Action>();
    public static readonly ConcurrentQueue<Action> _executionQueueFixedUpdate = new ConcurrentQueue<Action>();

    void Update()
    {
        while (_executionQueueUpdate.TryDequeue(out var action))
        {
            action?.Invoke();
        }
    }

    void FixedUpdate()
    {
        while (_executionQueueFixedUpdate.TryDequeue(out var action))
        {
            action?.Invoke();
        }
    }

    public static void EnqueueUpdate(Action action)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));

        _executionQueueUpdate.Enqueue(action);
    }

    public static void EnqueueFixedUpdate(Action action)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));

        _executionQueueFixedUpdate.Enqueue(action);
    }

    void OnDestroy()
    {
        _executionQueueUpdate.Clear();
        _executionQueueFixedUpdate.Clear();
    }
}