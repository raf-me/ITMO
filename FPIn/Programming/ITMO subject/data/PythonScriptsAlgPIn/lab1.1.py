#№1.1
import time

start_time = time.time()

def summa(lab):
    quantity = lab[0]
    bag = int(lab[1])  # количество места в сумке
    del lab[0]
    del lab[0]
    # Убираем показатели количества места в багаже

    list = [int(x) for x in lab]  #Создаём список с переменными целочисленного типа данных
    price_array = []  #Список цен каждого товара на один кг
    bag_array = []  #Список заполняемого багажа
    x = 0
    a: float
    for i in range(0, len(list), 2):
        a = list[i] / list[i + 1]
        price_array.append(a)  #Заполняем список цен каждого товара на один кг
        bag_array.append(list[i + 1])  #Количество веса на каждый товар заполняется одновремменно

    while bag != 0 and x >= 0:
        index = price_array.index(max(price_array))  #Находим самое выгодное предложение

        if bag >= bag_array[index]:  #Если количество места больше веса выгодного продукта
            bag -= bag_array[index]  #Убираем занятый весь
            x += bag_array[index] * price_array[index]  #Добавляем цену в зависимости от веса

        else:  #Если количество веса меньше выгодного продукта
            x += bag * price_array[index]  #Добавляем оставшийся продукт
            bag -= bag  #Убираем занятый вес (то есть приравниваем к нулю)

        del price_array[index]  #Удаляем самое выгодное предложение, чтобы перейти к следующему

    return print(x)  #Выводим итоговое значение


read = "1 10 500 30"
test = read.split(' ')  #Открываем и читаем файл.

list = [x for x in test]  #Заполняем список из test
lab = []

for i in list:  #Избавляемся от символа '\n', который разделяет строки в списке
    constant = i.split('\n')
    lab.extend(constant)

summa(lab)  #Применяем функцию к списку со значениями из файла

end_time = time.time()
execution_time = end_time - start_time
print(f"Время выполнения программы: {execution_time} секунд")


#№1.1
