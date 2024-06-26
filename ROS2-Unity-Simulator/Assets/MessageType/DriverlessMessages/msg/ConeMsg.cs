//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.DriverlessMessages
{
    [Serializable]
    public class ConeMsg : Message
    {
        public const string k_RosMessageName = "driverless_messages/Cone";
        public override string RosMessageName => k_RosMessageName;

        public const sbyte CONE_COLOR_YELLOW = 0;
        public const sbyte CONE_COLOR_BLUE = 1;
        public const sbyte CONE_COLOR_ORANGE_SMALL = 2;
        public const sbyte CONE_COLOR_ORANGE_BIG = 3;
        public const sbyte CONE_COLOR_GREEN = 4;
        public const sbyte CONE_COLOR_NO_CONE = -1;
        //  X position of the cone
        public float x;
        //  Y position of the cone
        public float y;
        //  Variances in both directions for this specific cone
        //  https://en.wikipedia.org/wiki/Variance
        public float variance_x = -1f;
        public float variance_y = -1f;
        //  Color of the cone
        public sbyte color = -1;
        //  Confidence that the prediction of cone color is correct
        public sbyte color_confidence = -1;

        public ConeMsg()
        {
            this.x = 0.0f;
            this.y = 0.0f;
        }

        public ConeMsg(float x, float y, float variance_x, float variance_y, sbyte color, sbyte color_confidence)
        {
            this.x = x;
            this.y = y;
            this.variance_x = variance_x;
            this.variance_y = variance_y;
            this.color = color;
            this.color_confidence = color_confidence;
        }

        public static ConeMsg Deserialize(MessageDeserializer deserializer) => new ConeMsg(deserializer);

        private ConeMsg(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.x);
            deserializer.Read(out this.y);
            deserializer.Read(out this.variance_x);
            deserializer.Read(out this.variance_y);
            deserializer.Read(out this.color);
            deserializer.Read(out this.color_confidence);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.x);
            serializer.Write(this.y);
            serializer.Write(this.variance_x);
            serializer.Write(this.variance_y);
            serializer.Write(this.color);
            serializer.Write(this.color_confidence);
        }

        public override string ToString()
        {
            return "ConeMsg: " +
            "\nx: " + x.ToString() +
            "\ny: " + y.ToString() +
            "\nvariance_x: " + variance_x.ToString() +
            "\nvariance_y: " + variance_y.ToString() +
            "\ncolor: " + color.ToString() +
            "\ncolor_confidence: " + color_confidence.ToString();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize);
        }
    }
}
