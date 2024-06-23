int8 CONE_COLOR_YELLOW = 0
int8 CONE_COLOR_BLUE = 1
int8 CONE_COLOR_ORANGE_SMALL = 2
int8 CONE_COLOR_ORANGE_BIG = 3
int8 CONE_COLOR_GREEN = 4
int8 CONE_COLOR_NO_CONE = -1

# X position of the cone
float32 x
# Y position of the cone
float32 y
# Variances in both directions for this specific cone
# https://en.wikipedia.org/wiki/Variance
float32 variance_x -1
float32 variance_y -1
# Color of the cone
int8 color -1
# Confidence that the prediction of cone color is correct
int8 color_confidence -1
