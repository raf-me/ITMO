import socket
import threading

HOST = "localhost"
PORT = 8080

def receive_message():
    reader = clientSocket.makefile("r", encoding="utf-8")

    try:
        while True:
            message = reader.readline()

            if message == "":
                print("\nСоединение с сервером закрыто.")
                break

            print(message.rstrip("\n"))

    except (OSError, UnicodeError) as error:
        print(f"\nОшибка при получении: {error}")

    finally:
        reader.close()


name = input("Введите имя: ").strip()

while not name:
    name = input("Имя не должно быть пустым. Введите имя: ").strip()

clientSocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
clientSocket.connect((HOST, PORT))
clientSocket.sendall((name + "\n").encode("utf-8"))

thread = threading.Thread(target=receive_message, daemon=True)
thread.start()

print("Вы подключились. Для выхода введите /exit")

try:
    while True:
        message = input()

        try:
            data = (message + "\n").encode("utf-8")
        except UnicodeEncodeError:
            print("Ошибка символов. Введите сообщение заново.")
            continue

        clientSocket.sendall(data)

        if message == "exit":
            break

except (OSError, EOFError, KeyboardInterrupt) as error:
    print(f"\nВыход из чата: {error}")

finally:
    try:
        clientSocket.shutdown(socket.SHUT_RDWR)
    except OSError:
        pass

    clientSocket.close()