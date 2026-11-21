import socket
import logging
import json
from urllib.parse import parse_qs
from html import escape

HOST = "localhost"
PORT = 8080

DATA_FILE = "/Users/rafael/PycharmProjects/WebL1/src/main/recources/ex5/grades.json"

logging.basicConfig(
    filename="/Users/rafael/PycharmProjects/WebL1/src/main/recources/ex5/server.log",
    level=logging.INFO,
    format="%(asctime)s %(levelname)s %(message)s",
    encoding="utf-8"
)

try:
    file = open(DATA_FILE, "r", encoding="utf-8")
    try:
        grades = json.load(file)

    finally:
        file.close()

except FileNotFoundError:
    grades = {}

def save_grades(grades):
    file = open(DATA_FILE, "w", encoding="utf-8")
    try:
        json.dump(grades, file, ensure_ascii=False, indent=4)

    finally:
        file.close()

def create_page():
    rows = ""
    for subject, marks in grades.items():
        marks_text = ", ".join(str(mark) for mark in marks)

        rows += (
            f"<tr><td>{escape(subject)}</td>\n"
            f"<td>{marks_text}</td></tr>\n"
        )

    file = open("/Users/rafael/PycharmProjects/WebL1/src/main/recources/ex5/index.html", "r", encoding="utf-8")
    template = file.read()
    file.close()

    return template.replace("{{rows}}", rows)

def send_response(connection, status, html, extra_headers=""):
    body = html.encode("utf-8")
    headers = (
        f"HTTP/1.1 {status}\r\n"
        "Content-Type: text/html; charset=utf-8\r\n"
        f"Content-Length: {len(body)}\r\n"
        "Connection: close\r\n"
        f"{extra_headers}"
        "\r\n"
    )

    connection.sendall(headers.encode("utf-8") + body)

serverSocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
serverSocket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
serverSocket.bind((HOST, PORT))
serverSocket.listen()

print(f"Сервер запущен: http://{HOST}:{PORT}")
logging.info("Сервер запущен")

while True:
    connection, address = serverSocket.accept()
    connection.settimeout(10)

    try:
        request = b""

        while b"\r\n\r\n" not in request:
            part = connection.recv(1024)
            if not part:
                break

            request += part

        if b"\r\n\r\n" not in request:
            continue

        headers_data, body = request.split(b"\r\n\r\n", 1)
        lines = headers_data.decode("iso-8859-1").split("\r\n")
        method, path, version = lines[0].split()

        headers = {}
        for line in lines[1:]:
            key, value = line.split(":", 1)
            headers[key.lower()] = value.strip()

        content_length = int(headers.get("content-length", "0"))

        if content_length < 0:
            raise ValueError("Некорректный Content-Length")

        while len(body) < content_length:
            part = connection.recv(min(4096, content_length - len(body)))

            if not part:
                raise ValueError("Тело запроса получено не полностью")

            body += part

        logging.info("%s %s от %s", method, path, address)

        if path != "/":
            send_response(connection, "404 Not Found", "Страница не найдена")

        elif method == "GET":
            send_response(connection, "200 OK", create_page())

        elif method == "POST":
            form = parse_qs(
                body[:content_length].decode("utf-8")
            )

            subject = form.get("subject", [""])[0].strip()
            grade_text = form.get("grades", [""])[0].strip()

            try:
                if not subject:
                    raise ValueError("Введите название дисциплины")

                new_grades = []

                for value in grade_text.split(","):
                    mark = int(value.strip())

                    if mark < 2 or mark > 5:
                        raise ValueError("Оценки должны быть от 2 до 5")

                    new_grades.append(mark)

            except ValueError:
                send_response(
                    connection,
                    "400 Bad Request",
                    "Введите дисциплину и оценки от 2 до 5 через запятую."
                    '<br><a href="/">Вернуться</a>'
                )
                continue

            if subject not in grades:
                grades[subject] = []

            grades[subject].extend(new_grades)
            save_grades(grades)

            logging.info(
                "Добавлены оценки %s по дисциплине %s",
                new_grades,
                subject
            )

            send_response(
                connection,
                "303 See Other",
                "",
                "Location: /\r\n"
            )

        else:
            send_response(
                connection,
                "405 Method Not Allowed",
                "Метод не поддерживается",
                "Allow: GET, POST\r\n"
            )

    except (ValueError, UnicodeError) as error:
        logging.warning("Некорректный запрос: %s", error)
        try:
            send_response(connection, "400 Bad Request", "Некорректный запрос")
        except OSError:
            pass

    except OSError as error:
        logging.exception("Ошибка соединения или файла: %s", error)
        print(f"Ошибка: {error}")

    finally:
        connection.close()
