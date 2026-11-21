import socket

HOST = "localhost"
PORT = 8080

serverSocket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverSocket.bind((HOST, PORT))

print(f"UDP сервер запущен на {HOST}:{PORT}")

while True:
    message, clientAddress = serverSocket.recvfrom(1024)
    print(f"Сообщение клиента: {message.decode('utf-8')}")

    serverSocket.sendto(
        "Hello, client!".encode('utf-8'),
        clientAddress
    )

    