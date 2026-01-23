import time
start_time = time.time()

def readfile(lab):
    with open(lab, 'r') as f:
        lines = f.read().splitlines()

    n, m = map(int, lines[0].split())  #Кол-во вершин и рёбер
    graph = [[] for _ in range(n)]

    for i in range(1, m + 1):
        a, b = map(int, lines[i].split())  #Считывает номера вершин
        graph[a - 1].append(b - 1)  #Добавляет вершину b-1 в список соседей вершины a-1 и наоборот
        graph[b - 1].append(a - 1)
	
    
    return graph



def graph(lab):
    n = len(lab)  #количество вершин
    vd = [False] * n  #массив посещенных вершин
    k = 0  #счетчик

    for start in range(n):  #цикл по всем вершинам
        if not vd[start]:
            stack = [start]  #Если вершина не посещена, инициализируем

            while stack:
                v = stack.pop()  #Извлечение вершины из стека

                if not vd[v]:
                    vd[v] = True  #Помечаем вершину как посещенную

                    for next in lab[v]:  #Перебор всех соседей текущей вершины
                        if not vd[next]:
                            stack.append(next)  #Добавление не посещенных соседей в стек

            k += 1
    return print(k)


lab = readfile("PythonScripts/Test3-2/test3-2.txt")
result = graph(lab)

end_time = time.time()
time = end_time - start_time
print(f"Время выполнения программы: {time} секунд")
