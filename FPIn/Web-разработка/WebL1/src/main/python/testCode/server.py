import socket

serverSocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
serverSocket.bind(('localhost', 8080))

serverSocket.listen(1)
print('Server running on port 8080')

while True:
    clientConnection, clientAddress = serverSocket.accept()
    clientConnection.setblocking(False)

    request = clientConnection.recv(1024).decode()
    print(f'Запрос от клиента: ', {request})

    response = 'Привет от сервера!'
    clientConnection.send(response.encode())

    clientConnection.close()

