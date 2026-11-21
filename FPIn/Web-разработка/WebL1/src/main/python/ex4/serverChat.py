import socket
import threading



HOST = "localhost"
PORT = 8080


users = {}
lock = threading.Lock()

def broadcast(message, sender):
    data = (message + "\n").encode("utf-8")
    lock.acquire()

    try:
        for connection in users:
            if connection != sender:
                try:
                    connection.sendall(data)

                except:
                    pass

    finally:
        lock.release()

def handle_client(connection, address):
    reader = connection.makefile("r", encoding="utf-8")
    name = None

    try:
        nickname = reader.readline().strip()
        if not nickname:
            return

        name = f"{nickname} ({address[1]})"

        lock.acquire()
        users[connection] = name
        lock.release()

        print(f"{name} вошёл в чат")
        broadcast(f"{name} вошёл в чат", connection)

        while True:
            message = reader.readline()

            if message == "":
                break

            message = message.rstrip("\n")

            if message == "exit":
                break

            print(f"{name}: {message}")
            broadcast(f"{name}: {message}", connection)

    except (OSError, UnicodeError):
        print(f"Соединение с {address} прервано")

    finally:
        lock.acquire()
        users.pop(connection, None)
        lock.release()

        reader.close()
        connection.close()

        if name is not None:
            print(f"{name} вышел из чата")
            broadcast(f"{name} вышел из чата", connection)


serverSocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
serverSocket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
serverSocket.bind((HOST, PORT))
serverSocket.listen()

print(f"Чат запущен на {HOST}:{PORT}")

while True:
    connection, address = serverSocket.accept()
    thread = threading.Thread(
        target=handle_client,
        args=(connection, address),
        daemon=True
    )

    thread.start()