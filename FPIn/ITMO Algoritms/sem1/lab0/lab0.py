import time

start_time = time.time()

read = (open(f'test1.txt'))
test = read.read().split(' ')
lab = [int(x) for x in test]


for i in range(len(lab), 2):
    if -(10 ** 9) <= (int(lab[i]) and int(lab[i+1])) <= 10 ** 9:
        first = int(lab[0]) + int(lab[1])
        print("Good!")
        with open('task1.txt', 'w') as file:
            file.write(str(first))
    else:
        print("Error!")


#Задание 1
if -(10**9) <= (int(test[0]) and int(test[1])) <= 10**9:
    first = int(test[0]) + int(test[1])
    print("Good!")
    with open('task1.txt', 'w') as file:
        file.write(str(first))
else:
    print("Error!")


#Задание 2
if -(10**9) <= (int(test[0]) and int(test[1])) <= 10**9:
    second = int(test[0]) + int(test[1])**2
    print("Good!")
    with open('task2.txt', 'w') as file:
        file.write(str(second))
else:
    print("Error!")

end_time = time.time()
execution_time = end_time - start_time
print(f"Время выполнения программы: {execution_time} секунд")

a = str('aaa')
b = str('maaa')
print(a, b)