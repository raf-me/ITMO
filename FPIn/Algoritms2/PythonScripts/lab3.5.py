import time
start_time = time.time()

def readfile(filename):
    with open(filename, 'r') as f:
        lines = f.read().splitlines()

    lines = sorted(lines[1:], key=lambda x: tuple(map(int, x.split())))
    lines.insert(0, f"{lines[-1].split()[0]} {len(lines)}")  # обновляем заголовок
    return lines


def task(data):
    n, m = map(int, data[0].split())
    graph = [[] for _ in range(n)]
    reverse_graph = [[] for _ in range(n)]

    for i in range(1, m + 1):
        u, v = map(int, data[i].split())
        graph[u - 1].append(v - 1)
        reverse_graph[v - 1].append(u - 1)

    visited = [False] * n
    order = []
    stack = []

    # Первый DFS (итеративный)
    for i in range(n):
        if not visited[i]:
            stack.append((i, 0))
            while stack:
                node, index = stack[-1]
                if not visited[node]:
                    visited[node] = True
                    for neighbor in graph[node]:
                        if not visited[neighbor]:
                            stack.append((neighbor, 0))
                    continue
                stack.pop()
                if node not in order:
                    order.append(node)

    visited = [False] * n
    count = 0
    stack = []

    # Второй DFS (по обратному графу)
    for i in reversed(order):
        if not visited[i]:
            stack.append(i)
            while stack:
                node = stack.pop()
                if not visited[node]:
                    visited[node] = True
                    for neighbor in reverse_graph[node]:
                        if not visited[neighbor]:
                            stack.append(neighbor)
            count += 1

    return print(count)

lab = readfile("PythonScripts/Test3-5/test3-5.txt")
print(task(lab))

end_time = time.time()
time = end_time - start_time
print(f"Время выполнения программы: {time} секунд")