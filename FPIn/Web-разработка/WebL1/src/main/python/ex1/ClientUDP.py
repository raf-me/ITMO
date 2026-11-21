import socket

HOST = "localhost"
PORT = 8080

client_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

client_socket.sendto(
    "Hello, client!".encode('utf-8'),
    (HOST, PORT)
)

message, server_address = client_socket.recvfrom(1024)
print(f"Ответ сервера: {message.decode('utf-8')}")

client_socket.close()