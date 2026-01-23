import time
start_time = time.time()

def readfile(lab):
    with open(lab, 'r') as f:
        lines = f.read().splitlines()

    n, m = map(int, lines[0].split())
    graph = [[] for _ in range(n)]

    for i in range(1, m + 1):
        u, v = map(int, lines[i].split())
        graph[u - 1].append(v - 1)

    return graph


def cycle(graph):
    n = len(graph)
    color = [0] * n

    for start in range(n):
        if color[start] == 0:
            stack = [(start, 0)]

            while stack:
                node, idx = stack[-1]
                if color[node] == 0:
                    color[node] = 1

                neabry = graph[node]
                if idx < len(neabry):
                    neabry = neabry[idx]
                    stack[-1] = (node, idx + 1)

                    if color[neabry] == 1:
                        return 1
                    elif color[neabry] == 0:
                        stack.append((neabry, 0))
                else:
                    color[node] = 2
                    stack.pop()

    return 0

lab = readfile("PythonScripts/Test3-3/test3-3.txt")
print(cycle(lab))

end_time = time.time()
time = end_time - start_time
print(f"Время выполнения программы: {time} секунд")