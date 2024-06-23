using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof(ConeGeneratorCSV))]
public class ConeGeneratorEditor : UnityEditor.Editor
{
    private ConeGeneratorCSV _coneGen;

    private void OnEnable()
    {
        _coneGen = (ConeGeneratorCSV)target;
    }


    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();

        PropertyField csvFile = new PropertyField() { bindingPath = "_csvFile" };
        PropertyField blueCone = new PropertyField() { bindingPath = "_blueConePrefab" };
        PropertyField yellowCone = new PropertyField() { bindingPath = "_yellowConePrefab" };
        
        Button generateTrackButton = new Button( () => _coneGen.GenerateTrackFromCSV(_coneGen._csvFile) )
        {
            text = "Generate Track"
        };

        root.Add(csvFile);
        root.Add(blueCone);
        root.Add(yellowCone);
        root.Add(generateTrackButton);
        
        return root;

        // base.CreateInspectorGUI()
        // _generateTrackButton = root.Q<Button>("GenerateTrackButton");

        // _generateTrackButton.RegisterCallback<ClickEvent>(GenerateTrack);

        // return root;
        
    }

    public void GenerateTrack(ClickEvent e)
    {
        _coneGen.GenerateTrackFromCSV(_coneGen._csvFile);
    }
}