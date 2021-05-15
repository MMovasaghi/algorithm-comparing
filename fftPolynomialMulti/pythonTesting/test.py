import numpy as np

a = [1,2,3,4] # 1*x^1 + 2*x^0
b = [4,1,3,1] # 3*x^1 + 4*x^0

afft = np.fft.rfft(a, 8)
bfft = np.fft.rfft(b, 8)
print(afft)
print(bfft)
y = afft*bfft
print(y)
r = np.fft.irfft(y)
print(r)