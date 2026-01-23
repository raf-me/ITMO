#№1.22
import time

start_time = time.time()

def matrix(lab):
    M, N = lab[0], lab[1]  # размеры матрицы
    total = 2 ** (M * N)  # всего возможных бинарных комбинаций
    x = 0

    for num in range(total):
        # Преобразуем число в бинарную строку нужной длины
        bin_str = bin(num)[2:].zfill(M * N)

        # Преобразуем в матрицу M×N
        matrix = []
        for i in range(M):
            row = []
            for j in range(N):
                row.append(int(bin_str[i * N + j]))
            matrix.append(row)

    if all(cell == 0 for row in matrix for cell in row):
        x += 1
    if all(cell == 1 for row in matrix for cell in row):
        x += 1

    k = total - x
    print(k)



read = (open(f'PythonScripts/Test1-22/Test1-22.txt'))
test = read.read().split(' ')  #Открываем и читаем файл.
lab = []
lab = [int(x) for x in test]

matrix(lab)

end_time = time.time()
execution_time = end_time - start_time
print(f"Время выполнения программы: {execution_time} секунд")