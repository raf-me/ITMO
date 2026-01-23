#№1.9
import time

start_time = time.time()

def minsumm(lab):
    # Коэффициенты для разного количества листов
    key = [1, 10, 100, 1000, 10000, 100000]

    helpsum = lab[0]  # Количество листов, которые нужно напечатать
    endsum = 0  # Создаём переменную для присваивания итогового значения
    del lab[0]

    # Считаем стоимость печати одного листа для каждого тарифа
    mins = []
    k = 0.1

    # Цикл для создания списка с ценами лист за рубль
    for i in lab:
        k = k * 10
        if helpsum > i:
            mins.append(i / k)

    while helpsum > 0:
        # Находим минимальную цену за лист
        min_price_index = mins.index(min(mins))

        # Находим наибольший возможный коэффициент (чтобы максимально "закрыть" helpsum)
        max_possible_count = helpsum // key[min_price_index]

        if max_possible_count > 0:
            # Печатаем максимально возможное количество по текущему тарифу
            endsum += max_possible_count * lab[min_price_index]
            helpsum -= max_possible_count * key[min_price_index]
        else:
            # Если уже невозможно напечатать по крупному тарифу — берём один комплект
            endsum += lab[min_price_index]
            helpsum -= key[min_price_index]

    return print(endsum)

read = (open(f'PythonScripts/Test1-9/Test1-9.txt'))
test = read.read().split(' ')  #Открываем и читаем файл.

list = [x for x in test]  #Заполняем список из test
lab = []

for i in list:  #Избавляемся от символа '\n', который разделяет строки в списке
    constant = i.split('\n')
    lab.extend(constant)

lab = [int(x) for x in lab]

end_time = time.time()
execution_time = end_time - start_time

minsumm(lab)
print(f"Время выполнения программы: {execution_time} секунд")

#№1.9