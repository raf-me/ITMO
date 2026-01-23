import time
start_time = time.time()

def readfile(lab):
    with open(lab, 'r') as file:
        return file.readline().strip()

def fun(s):
    n = len(s)
    pi = [0] * n
    for i in range(1, n):
        j = pi[i - 1]
        while j > 0 and s[i] != s[j]:
            j = pi[j - 1]
        if s[i] == s[j]:
            j += 1
        pi[i] = j
    print(*pi)

lab = readfile("PythonScripts/Test4-5/test4-5.txt")
fun(lab)

end_time = time.time()
time = end_time - start_time
print(f"Время выполнения программы: {time} секунд")
