import time
start_time = time.time()

def readfile(filename):
    with open(filename, 'r') as f:
        lines = f.read().splitlines()
    return lines[0], lines[1]

def fun(p, t):
    s = p + "#" + t
    n = len(s)
    z = [0] * n
    l, r = 0, 0

    for i in range(1, n):
        if i <= r:
            z[i] = min(r - i + 1, z[i - l])
        while i + z[i] < n and s[z[i]] == s[i + z[i]]:
            z[i] += 1
        if i + z[i] - 1 > r:
            l, r = i, i + z[i] - 1

    positions = []
    len_p = len(p)
    for i in range(len_p + 1, n):
        if z[i] == len_p:
            positions.append(i - len_p)

    print(len(positions))
    print(*[pos for pos in positions])

p, t = readfile("PythonScripts/Test4-1/test4-1.txt")
fun(p, t)

end_time = time.time()
time = end_time - start_time
print(f"Время выполнения программы: {time} секунд")
