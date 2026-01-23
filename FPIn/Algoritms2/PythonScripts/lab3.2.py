import time
start_time = time.time()

def readfile(lab):
    with open(lab, 'r') as f:
        lines = f.read().splitlines()

    n, m = map(int, lines[0].split())
    graph = [[] for _ in range(n)]

    for i in range(1, m + 1):
        a, b = map(int, lines[i].split())
        graph[a - 1].append(b - 1)
        graph[b - 1].append(a - 1)

    return graph

def graph(lab):
    n = len(lab)
    vd = [False] * n
    k = 0

    for start in range(n):
        if not vd[start]:
            stack = [start]

            while stack:
                v = stack.pop()

                if not vd[v]:
                    vd[v] = True

                    for next in lab[v]:
                        if not vd[next]:
                            stack.append(next)

            k += 1
    return print(k)


lab = readfile("PythonScripts/Test3-2/test3-2.txt")
result = graph(lab)

end_time = time.time()
time = end_time - start_time
print(f"Время выполнения программы: {time} секунд")