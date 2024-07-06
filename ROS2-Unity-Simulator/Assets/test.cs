using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class test : ScriptableObject
{
     public int m_Field = 1;

    [MenuItem("Example/SerializedObject Update")]
    static void UpdateExample()
    {
        var scriptableObject = ScriptableObject.CreateInstance<test>();
        var sb = new StringBuilder();

        var serializedObject = new SerializedObject(scriptableObject);
            SerializedProperty field = serializedObject.FindProperty("m_Field");

            // Change underlying object
            scriptableObject.m_Field = 2;

            // SerializedObject still thinks value is 1
            sb.Append($"SerializedObject value before Update: {field.intValue} ");

            //hasModifiedProperties returns false because no changes have been made via SerializedProperty API
            sb.Append($"(SerializedObject dirty: {serializedObject.hasModifiedProperties}), ");

            // Update so that SerializedObject sees the new value
            serializedObject.Update();
            sb.AppendLine($"after Update: {field.intValue}");

            // Another scenario is when Update is called while there are pending changes in the SerializedObject
            field.intValue = 3;
            sb.Append($"SerializedObject value before Update: {field.intValue} ");
            sb.Append($"(SerializedObject dirty: {serializedObject.hasModifiedProperties}), ");

            // Value reverts back to 2, because ApplyModifiedProperties was not called
            // and SerializedObject has been put back in sync with the object's state
            serializedObject.Update();
            sb.AppendLine($"after Update: {field.intValue}. (Dirty: {serializedObject.hasModifiedProperties})");

            Debug.Log(sb.ToString());
        
    }
}
