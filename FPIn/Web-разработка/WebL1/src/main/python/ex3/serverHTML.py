import socket

HOST, PORT = "localhost", 8080

server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
server_socket.bind((HOST, PORT))

server_socket.listen()

print(f"Сервер запущен: http://{HOST}:{PORT}")

while True:
    client_socket, client_address = server_socket.accept()
    print(f"Подключился клиент: {client_address}")
    result = client_socket.recv(1024).decode("utf-8")

    print(f"Запрос:\n{result}")
    htmlFile = open(""
                    "/Users/rafael/PycharmProjects/WebL1/src/main/recources/ex3/index.html",
                    "r",
                    encoding="utf-8"
                    )

    htmlContent = htmlFile.read()
    htmlFile.close()

    body = htmlContent.encode("utf-8")

    headers = (
        "HTTP/1.1 200 OK\r\n"
        "Content-Type: text/html; charset=utf-8\r\n"
        f"Content-Length: {len(body)}\r\n"
        "Connection: close\r\n"
        "\r\n"
    )

    client_socket.sendall(headers.encode("utf-8") + body)
    client_socket.close()