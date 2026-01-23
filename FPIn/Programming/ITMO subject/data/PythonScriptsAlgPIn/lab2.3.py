import time

start_time = time.time()

def optim(lab):
    sort = []
    for i in range(0, len(lab)):
        if lab[i] == '+':
            first = int(lab[i+1])
            sort.append(first)

        if lab[i] == '>':
            second = int(lab[i+1])
            s = [x for x in sort if x > second]
            if sort == [] or sort == [" " * i]:
                print("Список пустой.")
            elif second >= max(sort):
                s.append(0)
            print(min(s))

read = open("PythonScripts/Test2-3/test2-3.txt")
test = read.read().split(' ')  #Открываем и читаем файл.

list = [x for x in test]  #Заполняем список из test
lab = []

for i in list:  #Избавляемся от символа '\n', который разделяет строки в списке
    constant = i.split('\n')
    lab.extend(constant)

lab = [x for x in lab]
optim(lab)

end_time = time.time()
time = end_time - start_time
print(f"Время выполнения программы: {time} секунд")