from collections import deque
import time
start_time = time.time()

def readfile(filename):
    with open(filename, 'r') as f:
        lines = f.read().splitlines()

    return lines


def graph(lab):
    n, m = map(int, lab[0].split())
    graph = [[] for _ in range(n)]

    for i in range(1, m + 1):
        u, v = map(int, lab[i].split())
        graph[u - 1].append(v - 1)
        graph[v - 1].append(u - 1)

    color = [-1] * n  # -1 = не раскрашен, 0 и 1 — цвета

    for start in range(n):
        if color[start] == -1:
            queue = deque()
            queue.append(start)
            color[start] = 0

            while queue:
                node = queue.popleft()
                for neighbor in graph[node]:
                    if color[neighbor] == -1:
                        color[neighbor] = 1 - color[node]
                        queue.append(neighbor)
                    elif color[neighbor] == color[node]:
                        print(0)
                        return

    print(1)

lab = readfile("PythonScripts/Test3-7/test3-7.txt")
graph(lab)

end_time = time.time()
time = end_time - start_time
print(f"Время выполнения программы: {time} секунд")
