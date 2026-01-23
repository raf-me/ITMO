def inver(x):
    a = ''
    for i in x:
        if i == '1':
            a += '0'
        elif i == '0':
            a += '1'
    a = '1' + a
    return a

def invers2(x):
    if x > 0:
        return hex(x)[2:]
    elif x < 0:
        a = hex(x)
        a = a.replace('0x', '')
        return a

def binary(lab):
    if len(lab) != 2:
        print('Недопустимое количество вводимых чисел')
    else:
        A = lab[0]
        C = lab[1]

        task1 = bin(A)[2:]
        task2 = bin(C)[2:]
        task3 = bin(A + C)[2:]
        task4 = bin(A + C + C)[2:]
        task5 = bin(C - A)[2:]
        task6 = bin(65536 - (C - A))[2:]
        task7 = inver(task1)
        task8 = inver(task2)
        task9 = inver(task3)
        task10 = inver(task4)
        task11 = inver(task5)
        task12 = inver(task6)
        taskB1 = invers2((A+C))
        taskB2 = invers2((A + C + C))
        taskB3 = invers2((-A - C))
        taskB4 = invers2((-A - C - C))
        taskB5 = invers2((C - A))
        taskB6 = invers2((A - C))


        task = [task1, task2, task3,
                task4, task5, task6,
                task7, task8, task9,
                task10, task11, task12,
                taskB1, taskB2, taskB3,
                taskB4, taskB5, taskB6]

        for i in task:
            print(i)


lab = [2774, 15388]  #Заполняем список из test

binary(lab)

def f(c, b):
    one = int(str(c), 16)
    two = int(str(b), 16)
    x = one-two
    a = bin(x)[2:]
    if b - c < 0:
        a = inver(a)
        return print(hex(int(a, 2))[2:])
    else:
        return print(hex(int(a, 2))[2:])


f(2295, 1377)
