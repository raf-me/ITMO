#№1.17
import time
start_time = time.time()

def number(lab):
    C = 10 ** 9 + 7

    mat = {
    0: [4, 6],
    1: [6, 8],
    2: [7, 9],
    3: [4, 8],
    4: [3, 9, 0],
    5: [],
    6: [1, 7, 0],
    7: [2, 6],
    8: [1, 3],
    9: [2, 4]
    }  #Создаём готовые варианты ходов коня

    for i in range(len(lab)):  #Если на входном файле несколько чисел
        n = lab[i]

        if n == 1:
            return print(8)  #Если значение входного файла 1, то возможно только 8 вариаций

        komb = [0] * 10  #Начальный массив, который будет пребирать значения возможных вариантов

        for j in range(10):  #Исключаем недопустимые значения
            if j != 0 and j != 8:
                komb[j] = 1
            else:
                komb[j] = 0

        for _ in range(n - 1):  #Перебираем все значения до n
            new_komb = [0] * 10  #Создаём массив, куда мы будем записывать итоговый вариант

            for key in range(10):  #Перебираем ключи для mat
                for move in mat[key]:  #Перебираем значения из ячейки по ключу
                    new_komb[move] = (new_komb[move] + komb[key]) % C

            # Обновляем массив
            komb = new_komb

        # Суммируем все возможные пути
        return print(sum(komb) % C, komb)



read = (open(f'PythonScripts/Test1-17/Test1-17.txt'))
test = read.read().split(' ')  #Открываем и читаем файл.
lab = [int(x) for x in test]

end_time = time.time()
execution_time = end_time - start_time

number(lab)
print(f"Время выполнения программы: {execution_time} секунд")
