import socket

HOST = 'localHost'
PORT = 8080

server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_socket.bind((HOST, PORT))

server_socket.listen(5)

print(f"HTTP сервер запущен на {HOST}:{PORT}...")

html_content = '''
<!DOCTYPE html>
<html>
<head>
    <title>Простой сервер на Python</title>
</head>
<body>
    <h1>Привет! Это простая HTML-страница.</h1>
    <p>Этот сервер написан на Python и работает через сокеты.</p>
</body>
</html>
'''

while True:
    client_socket, client_address = server_socket.accept()
    print(f'Подключение от {client_address}')

    request = client_socket.recv(1024).decode()
    print(f'Запрос клиента:\n{request}')

    http_response = (
        "HTTP/1.1 200 OK\r\n"
        "Content-Type: text/html; charset=UTF-8\r\n"
        f"Content-Length: {len(html_content)}\r\n"
        "Content: close\r\n"
        "\r\n"
        + html_content
    )

    client_socket.sendall(http_response.encode())
    client_socket.close()
