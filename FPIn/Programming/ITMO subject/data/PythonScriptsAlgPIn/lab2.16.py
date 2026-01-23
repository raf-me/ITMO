import time

start_time = time.time()

with open("PythonScripts/Test2-16/test2-16.txt", "r") as fin:
    test = [line.strip() for line in fin if line.strip()]

# Не полагаемся на n, просто берём все команды
lab = test[1:] if test[0].isdigit() else test

data = []

# Ручной бинарный поиск позиции
def binary_search(arr, x):
    left, right = 0, len(arr)

    while left < right:
        mid = (left + right) // 2

        if arr[mid] < x:
            left = mid + 1

        else:
            right = mid

    return left

# Обработка команд
for cmd in lab:
    parts = cmd.replace('+', '').split()

    if len(parts) < 2:
        continue  # на всякий случай
    c, x = int(parts[0]), int(parts[1])

    if c == 1:
        idx = binary_search(data, x)

        if idx == len(data) or data[idx] != x:
            data.insert(idx, x)

    elif c == -1:
        idx = binary_search(data, x)

        if idx < len(data) and data[idx] == x:
            data.pop(idx)

    elif c == 0:
        print(data[-x])  # k-й по убыванию

end_time = time.time()
time = end_time - start_time
print(f"Время выполнения программы: {time} секунд")