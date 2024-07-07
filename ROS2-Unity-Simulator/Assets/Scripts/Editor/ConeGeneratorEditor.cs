using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof(ConeGeneratorCSV))]
public class ConeGeneratorEditor : UnityEditor.Editor
{
    private ConeGeneratorCSV _coneGen;

    private void Awake()
    {
        _coneGen = (ConeGeneratorCSV)target;
    }


    public override VisualElement CreateInspectorGUI()
    {
        VisualElement _root = new VisualElement();

        PropertyField skipColumnNamesRow = new PropertyField() { bindingPath = "skipColumnNamesRow" };
        PropertyField csvFile = new PropertyField() { bindingPath = "csvFile" };
        PropertyField blueCone = new PropertyField() { bindingPath = "blueConePrefab" };
        PropertyField yellowCone = new PropertyField() { bindingPath = "yellowConePrefab" };
        PropertyField noColorSpecifiedPrefab = new PropertyField() { bindingPath = "noColorSpecifiedPrefab" };
        
        Button generateTrackButton = new Button( () => _coneGen.GenerateTrackFromCSV(_coneGen.csvFile) )
        {
            text = "Generate Track"
        };


        Button destroyTrackButton = new Button( () => _coneGen.RemoveOldCones())
        {
            text = "Destroy Track"
        };

        _root.Add(skipColumnNamesRow);
        _root.Add(csvFile);
        _root.Add(blueCone);
        _root.Add(yellowCone);
        _root.Add(noColorSpecifiedPrefab);
        _root.Add(generateTrackButton);
        _root.Add(destroyTrackButton);

        _root.Bind(new SerializedObject(target));
        
        return _root;
    }

}