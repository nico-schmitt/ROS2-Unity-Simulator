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
1. Start the ros-unity-endpoint:
```
./startDefaultEndpoint
```
2. Click on play button in Unity Editor (top left connection icon should be blue if endpoint is running)
3. Start the Autonomous System Management executable:
```
ros2 run autonomous_system_management asm
```
and start the simulator executable as described in the [simulator repository](https://gitlab.dynamics-regensburg.de/dynamics/driverless/rpd23/simulator):
```
ros2 run simulator simulator
```
