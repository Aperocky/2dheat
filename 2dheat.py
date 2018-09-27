import numpy as np
import matplotlib.pyplot as plt
import matplotlib.animation as animation

# Calculate 2d Heat diffusion

# Plate size (100, 100)

# Assume conductivity = 1

# The heat input from plate[0] = 1.0, no heat loss at any other edge

# Difference in x and y

dx2 = dy2 = 0.01

dt = 0.0005 # Fine T steps

# Timestep tables from within

def timestep(plate):
    res = plate.copy()
    plate[0, :] = plate[1, :]
    plate[-1, :] = plate[-2, :]
    plate[1:-1, 0] = plate[1:-1, 1]
    plate[1:-1, -1] = plate[1:-1, -2]
    xdiff = (plate[2:, 1:-1] - 2*plate[1:-1, 1:-1] + plate[:-2, 1:-1])/dx2
    # xdiff[0, :] = 1
    ydiff = (plate[1:-1, 2:] - 2*plate[1:-1, 1:-1] + plate[1:-1, :-2])/dy2
    # ydiff[1:, 0] = 1
    res[1:-1, 1:-1] = plate[1:-1, 1:-1] + dt * (xdiff + ydiff)
    return res

fig = plt.figure()
ax = fig.add_subplot(111)

plate = np.zeros((102,102))
for i in range(102):
    for j in range(102):
        if (i - 50)**2 + (j - 50)**2 < 401:
            plate[i,j] = 10
im = plt.imshow(plate[1:-1, 1:-1], vmin=0, vmax=5, interpolation="none")
# for i in range(2000):
#     plate = timestep(plate)
#     if i % 20 == 0:
#         print("Processing step {}".format(i))
#         plt.imshow(plate[1:-1, 1:-1])
#         plt.show()

def init():
    im.set_data(plate[1:-1, 1:-1])
    return [im]

def animate(number):
    global plate
    print(number)
    for i in range(20):
        plate = timestep(plate)
    im.set_data(plate[1:-1, 1:-1])
    # print(plate[1:20, 1:20])
    return [im]

anim = animation.FuncAnimation(fig, animate, init_func=init, \
                               frames=500, interval=20, blit=True)

plt.show()