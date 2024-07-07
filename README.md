# Driverless Unity simulator for visualization

## Setup
1. Navigate to cloned repository and go inside docker folder and run to build docker image:
```
docker build -t unity_ros_endpoint .
```
2. Start container from image:
```
docker run -it --rm -p 10000:10000 unity_ros_endpoint
```
## How to run simulation
1. Start the unity-ros-endpoint:
```
./startDefaultEndpoint
```
2. Click on play button in Unity Editor (top left connection icon should be blue if endpoint is running)
3. Select the SimulationManager in the left hierarchy. Configure the topics that should be displayed and select a test track
4. Click the "Start Simulator" button to see the magic in 4K
5. To exit **first** click the "Stop Simulator" button and **then** quit the editor. If done in another order there might be issues and you have to manually kill the processes in the runnning docker container:
```
pkill -9 asm
pkill -9 simulator
```
If issues still persist after this just restart everything

## Additional hints
- The unity-ros-endpoint might crash sometimes. Just run it again the container with `./startDefaultEndpoint`
- If cones are drawn even though the simulator button has not been clicked, then the simulator exe is probably still running and you have to manually kill the process with `pkill -9 simulator`
