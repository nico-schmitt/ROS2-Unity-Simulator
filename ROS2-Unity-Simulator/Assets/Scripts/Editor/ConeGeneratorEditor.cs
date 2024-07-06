using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Unity.VisualScripting;

[CustomEditor(typeof(ConeGeneratorCSV))]
public class ConeGeneratorEditor : UnityEditor.Editor
{
    private VisualElement root;
    private ConeGeneratorCSV _coneGen;

    private void Awake()
    {
        _coneGen = (ConeGeneratorCSV)target;
    }


    public override VisualElement CreateInspectorGUI()
    {
        root = new VisualElement();

        PropertyField enableGenerator = new PropertyField() { bindingPath = "enableGenerator" };
        PropertyField csvFile = new PropertyField() { bindingPath = "csvFile" };
        PropertyField blueCone = new PropertyField() { bindingPath = "blueConePrefab" };
        PropertyField yellowCone = new PropertyField() { bindingPath = "yellowConePrefab" };
        
        Button generateTrackButton = new Button( () => _coneGen.GenerateTrackFromCSV(_coneGen.csvFile) )
        {
            text = "Generate Track"
        };
        generateTrackButton.SetEnabled(_coneGen.enableGenerator);

        enableGenerator.RegisterCallback<ChangeEvent<bool>>(evt => {
            generateTrackButton.SetEnabled(_coneGen.enableGenerator);
            _coneGen.RemoveOldCones();
        });
        
        root.Add(enableGenerator);
        root.Add(csvFile);
        root.Add(blueCone);
        root.Add(yellowCone);
        root.Add(generateTrackButton);

        root.Bind(new SerializedObject(target));
        
        return root;
    }

}