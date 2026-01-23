import time
start_time = time.time()

def readfile(lab):
    with open(lab, 'r') as file:
        return file.readline().strip()


def fun(s):
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

    print(*z[1:])



lab = readfile("PythonScripts/Test4-6/test4-6.txt")
fun(lab)

end_time = time.time()
time = end_time - start_time
print(f"Время выполнения программы: {time} секунд")

