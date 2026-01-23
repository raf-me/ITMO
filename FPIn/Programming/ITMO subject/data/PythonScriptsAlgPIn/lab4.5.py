import time
start_time = time.time()

def readfile(lab):
    with open(lab, 'r') as file:
        return file.readline().strip()
   

def fun(lab):
    n = len(lab) #длина строки
    pi = [0] * n  #массив для хранения значений префикс-функции
    for i in range(1, n):
        j = pi[i - 1]  #переменная-указатель, инициализируется предыдущим значением префикс
        while j > 0 and lab[i] != lab[j]:  #Поиск наибольшего префикса
            j = pi[j - 1]  #Уменьшаем j, пока не найдем совпадение символов
        if lab[i] == lab[j]:
            j += 1  #Если символы совпали - увеличиваем длину совпадающего префикса
        pi[i] = j  #Записываем результат в массив pi
    print(*pi)
    

lab = readfile("PythonScripts/Test4-5/test4-5.txt")
fun(lab)

end_time = time.time()
time = end_time - start_time
print(f"Время выполнения программы: {time} секунд")
