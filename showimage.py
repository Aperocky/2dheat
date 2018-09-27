import numpy as np
import matplotlib.pyplot as plt

my_array = np.genfromtxt('data.csv', delimiter=",")

plt.imshow(my_array)
plt.show()
