#№1.18
import time

start_time = time.time()

def cafe(lab):
    n = lab[0]

    # Считываем стоимости обедов для n дней
    costs = lab[1:]

    if len(costs) != n:
        raise ValueError("Неверное количество значений!")

    # value[i][j] — минимальная стоимость за i дней при наличии j купонов
    value = [[float('inf')] * (n + 1) for _ in range(n + 1)]
    value[0][0] = 0

    # Восстановление траектории
    used_coupons = [[False] * (n + 1) for _ in range(n + 1)]

    for i in range(n):
        for coupons in range(n + 1):
            # 1. Покупаем обед за деньги
            value[i + 1][coupons] = min(value[i + 1][coupons], value[i][coupons] + costs[i])

            # 2. Получаем купон, если обед стоит больше 100 рублей
            if costs[i] > 100 and coupons < n:
                if value[i + 1][coupons + 1] > value[i][coupons]:
                    value[i + 1][coupons + 1] = value[i][coupons]
                    used_coupons[i + 1][coupons + 1] = False

            # 3. Используем купон, если он у нас есть
            if coupons > 0:
                if value[i + 1][coupons - 1] > value[i][coupons]:
                    value[i + 1][coupons - 1] = value[i][coupons]
                    used_coupons[i + 1][coupons - 1] = True

    # Минимальная сумма
    min_cost = min(value[n])

    # Восстанавливаем путь (дни, в которые использовались купоны)
    coupons = value[n].index(min_cost)
    used_days = []

    for i in range(n, 0, -1):
        if used_coupons[i][coupons]:
            used_days.append(i)
            coupons += 1

    used_days.reverse()

    # Количество оставшихся купонов и использованных купонов
    remaining_coupons = coupons
    used_coupons_count = len(used_days)

    # Результат
    print(min_cost)
    print(remaining_coupons, used_coupons_count)
    if used_coupons_count > 0:
        print(*used_days)


read = (open(f'PythonScripts/Test1-18/Test1-18.txt'))
test = read.read().split(' ')  #Открываем и читаем файл.

list = [x for x in test]  #Заполняем список из test
lab = []

for i in list:  #Избавляемся от символа '\n', который разделяет строки в списке
    constant = i.split('\n')
    lab.extend(constant)

lab = [int(x) for x in lab]

print(lab)

end_time = time.time()
execution_time = end_time - start_time
print(f"Время выполнения программы: {execution_time} секунд")