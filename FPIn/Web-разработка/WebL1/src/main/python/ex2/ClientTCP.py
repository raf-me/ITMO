import socket

HOST = "localhost"
PORT = 8080

clientSocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
clientSocket.connect((HOST, PORT))
print("Подключились к серверу")

a = float(input("Введите a: "))
b = float(input("Введите b: "))
c = float(input("Введите c: "))

message = f"{a} {b} {c}"
clientSocket.sendall(message.encode("utf-8"))

response = clientSocket.recv(1024).decode("utf-8")
print(f"Ответ сервера: {response}")

clientSocket.close()