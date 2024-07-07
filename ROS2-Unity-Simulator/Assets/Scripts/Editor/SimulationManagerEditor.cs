using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof(SimulationManager))]
public class SimulationManagerEditor : Editor
{
    private SimulationManager _simManager;

    private void Awake()
    {
        _simManager = (SimulationManager)target;
    }

    public override VisualElement CreateInspectorGUI()
    {
        PropertyField curTrack = new PropertyField() { bindingPath = "_currentTrack"};

        VisualElement root = new VisualElement();
        Button startSimButton = new Button(_simManager.StartSimulator)
        {
            text = "Start Simulator"
        };
        startSimButton.SetEnabled(!_simManager.IsPlaying);
        Button stopSimButton = new Button(_simManager.StopSimulator)
        {
            text = "Stop Simulator"
        };
        stopSimButton.SetEnabled(_simManager.IsPlaying);

        startSimButton.RegisterCallback<ClickEvent>(evt => {
            startSimButton.SetEnabled(false);
            stopSimButton.SetEnabled(true);
        });
        stopSimButton.RegisterCallback<ClickEvent>(evt => {
            stopSimButton.SetEnabled(false);
            startSimButton.SetEnabled(true);
        });

        root.Add(curTrack);
        root.Add(startSimButton);
        root.Add(stopSimButton);

        return root;
    }
}
