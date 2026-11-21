import socket

HOST = "localhost"
PORT = 8080

clientSocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
clientSocket.connect((HOST, PORT))

print("Подключение к серверу.")

request = (
    "GET / HTTP/1.1\r\n"
    f"Host: {HOST}:{PORT}\r\n"
    "Connection: close\r\n"
    "\r\n"
)

clientSocket.sendall(request.encode("utf-8"))
response = b""

while True:
    part = clientSocket.recv(1024)
    if not part:
        break

    response += part

print(response.decode("utf-8"))
clientSocket.close()