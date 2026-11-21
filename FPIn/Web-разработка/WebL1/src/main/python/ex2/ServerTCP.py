import math
import socket

HOST = "localhost"
PORT = 8080

serverSocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
serverSocket.bind((HOST, PORT))
serverSocket.listen(1)
print("Сервер запущен на порту 8080")

while True:
    clientConnection, clientAddress = serverSocket.accept()
    print(f"Подключился клиент: {clientAddress}")

    request = clientConnection.recv(1024).decode("utf-8")
    print(f"Получено: {request}")

    a, b, c = map(float, request.split())

    if a == 0:
        result = "Это не квадратное уравнение: a не должно быть равно 0"
    else:
        d = b ** 2 - 4 * a * c

        if d < 0:
            result = "Действительных корней нет"

        elif d == 0:
            x = -b / (2 * a)
            result = f"x = {x}"

        else:
            x1 = (-b + math.sqrt(d)) / (2 * a)
            x2 = (-b - math.sqrt(d)) / (2 * a)
            result = f"x1 = {x1}, x2 = {x2}"

    clientConnection.sendall(result.encode("utf-8"))
    clientConnection.close()