FROM ros:humble-ros-base

# Make ROS2 Workspace Dirss
RUN mkdir -p /home/dev_ws/src

# Copy driverless message package into workspace
COPY ./ros2_packages /home/dev_ws/src/
# Copy function to be able to unsubscribe
COPY ./remove_subscriberPyFunc /home/dev_ws/install/ros_tcp_endpoint/lib/python3.10/site-packages/ros_tcp_endpoint/
# Copy script to launch default endpoint easier
COPY ./startDefaultEndpoint.sh /home/dev_ws/

#Check out ROS-TCP-Endpoint, ROS2 version
RUN git clone https://github.com/Unity-Technologies/ROS-TCP-Endpoint /home/dev_ws/src/ros_tcp_endpoint -b ROS2v0.7.0

# Change to workspace on sign in
RUN echo "cd home/dev_ws" >> ~/.bashrc

# Build the workspace
RUN cd home/dev_ws && . /opt/ros/humble/setup.sh && colcon build

## Add unsubscribe function to server
RUN cd /home/dev_ws/install/ros_tcp_endpoint/lib/python3.10/site-packages/ros_tcp_endpoint && cat remove_subscriberPyFunc >> server.py

# Source the workspace on sign in
RUN echo ". install/setup.bash" >> ~/.bashrc
RUN echo ". install/local_setup.bash" >> ~/.bashrc
