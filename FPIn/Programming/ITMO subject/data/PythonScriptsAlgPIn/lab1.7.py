#N1.7
import time

start_time = time.time()

def maximum(lab):
    max_time = int(lab[0])
    quantity = int(lab[1])  # количество места в сумке
    del lab[0]
    del lab[0]
    # Убираем показатели количества времени в количество ботинок

    list = [int(x) for x in lab]  # Создаём список с переменными целочисленного типа данных
    final: float
    final = 0.0

    while final < max_time:
        #Создаём цикл, в котором мы увеличиваем количество потраченного времени и уменьшаем количество доступного

        final += min(list)  #Чтобы отремонтировать наибольшее кол-во сапог, лучше починить те, на которые уйдёт меньше времени
        list.remove(min(list))  #Удаляем минимальный элемент, для повторного прохождения цикла
        max_time -= final  #Уменьшаем доступное время

    return print(final)

read = (open("data/PythonScriptsAlgPIn/Test1-7/Test1-7.txt"))
test = read.read().split(' ')  #Открываем и читаем файл.

list = [x for x in test]  #Заполняем список из test
lab = []

for i in list:  #Избавляемся от символа '\n', который разделяет строки в списке
    constant = i.split('\n')
    lab.extend(constant)

maximum(lab)

end_time = time.time()
execution_time = end_time - start_time
print(f"Время выполнения программы: {execution_time} секунд")

#N1.7
