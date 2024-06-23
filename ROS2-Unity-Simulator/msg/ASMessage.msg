builtin_interfaces/Time stamp
# Ordered list of the indices of values that are set in the values list
int8[] set_values
# Ordered list of the values corresponding to the entries in set_values
float64[] values

# Example:
#   set_values = [KEEP_DRIVING, LAP_COUNTER, ALL_NODES_STARTED, GO_RECEIVED]
#   values     = [1.0         , 7.0        , 1.0              , 1.0        ]
# Means that:
#   KEEP_DRIVING        = 1.0
#   LAP_COUNTER         = 7.0
#   ALL_NODES_STARTED   = 1.0
#   GO_RECEIVED         = 1.0

# If the car should keep driving
int8 KEEP_DRIVING = 0
# If the current mission was successfully finished
int8 MISSION_FINISHED = 1
# Laps completed, starts at 0
int8 LAP_COUNTER = 2
# If any Fatal Error has been noticed
int8 ERROR_FATAL = 3
# If all necessary nodes have been started
int8 ALL_NODES_STARTED = 4
# If the go signal from the VCU has been received
int8 GO_RECEIVED = 5
