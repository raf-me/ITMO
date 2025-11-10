Отчёт по лабораторной работе 1: 
Настройка Nginx и HTTPS с виртуальными хостами
Цель
Настроить веб-сервер Nginx так, чтобы:
Он обслуживал два pet-проекта по HTTPS.
Обеспечивал перенаправление с HTTP на HTTPS (порт 80 -> 443).
Использовал alias для псевдонимов путей.
Разграничивал проекты по доменным именам (виртуальные хосты).


Установка и запуск Nginx на MacOS (через Homebrew)
Команды:
brew install nginx
brew services start nginx

brew install nginx - устанавливает Nginx.
brew services start nginx - запускает его как фоновый сервис.

Проверка:
nginx -t
brew services list
lsof -i -n -P | grep nginx

nginx -t - проверка конфигурации.
lsof - проверка портов (8080, 80, 443).


Настройка HTML-страниц 
Созданы директории и минимальные данные в  index.html:

BREW="$(brew --prefix)"
mkdir -p "$BREW/var/www/site1/html" "$BREW/var/www/site2/html"
echo "<h1>Hello World</h1>"  > "$BREW/var/www/site1/html/index.html"
echo "<h1>Hello DevOps</h1>" > "$BREW/var/www/site2/html/index.html"

Также добавлен alias-каталог:

mkdir -p "$BREW/var/www/site1/assets_src"
echo "asset ok" > "$BREW/var/www/site1/assets_src/info.txt"


Настройка HTTPS
Установлен mkcert и сгенерированы локальные сертификаты (Подключается к серверу, сервер отдаёт сертификат, браузер проверяет: 1) действителен ли сертификат сейчас по датам, 2) на какое имя он выдан (домен), 3) доверяет ли он тому, кто его подписал (CA — центр сертификации)):
mkcert -install
mkcert -cert-file "$CERT_DIR/site1.local.pem" -key-file "$CERT_DIR/site1.local-key.pem" site1.local localhost 127.0.0.1 ::1
mkcert -cert-file "$CERT_DIR/site2.local.pem" -key-file "$CERT_DIR/site2.local-key.pem" site2.local localhost 127.0.0.1 ::1


Локальные хосты:
sudo sh -c 'printf "\n127.0.0.1 site1.local\n127.0.0.1 site2.local\n" >> /etc/hosts'


Конфигурация виртуальных хостов (site1.conf и site2.conf)
Созданы файлы:
sudo touch /opt/homebrew/etc/nginx/servers/site1.conf
sudo chown rafael:admin /opt/homebrew/etc/nginx/servers/site1.conf

Содержимое site1.conf:
server {
    listen 80;
    server_name site1.local;
    return 301 https://$host$request_uri;
}

server {
    listen 443 ssl;
    server_name site1.local;

    ssl_certificate      /opt/homebrew/etc/nginx/certs/site1.local.pem;
    ssl_certificate_key  /opt/homebrew/etc/nginx/certs/site1.local-key.pem;

    root /opt/homebrew/var/www/site1/html;
    index index.html;

    location /assets/ {
        alias /opt/homebrew/var/www/site1/assets_src/;
        autoindex off;
        try_files $uri $uri/ =404;
    }

    location / {
        proxy_pass http://127.0.0.1:9000;
        proxy_set_header Host              $host;
        proxy_set_header X-Real-IP         $remote_addr;
        proxy_set_header X-Forwarded-For   $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}


Описание конфигурация:
listen 80; return 301 https://... - принудительно перенаправляет HTTP на HTTPS.
listen 443 ssl; - включает HTTPS.
ssl_certificate / ssl_certificate_key - путь к сертификату и ключу.
server_name - определяет, на какой домен реагировать.
root - корень файлового сервера.
index - стартовый HTML-файл.
location /assets/ - псевдоним через alias.
proxy_pass - проксирование запросов на Python-сервер (порт 9000).

Аналогично настроен site2.conf (без alias и прокси).

Проверка

rafael@Rafaels-MacBook-Pro html % curl -I http://site1.local
HTTP/1.1 301 Moved Permanently
Server: nginx/1.29.2
Date: Sun, 09 Nov 2025 21:30:41 GMT
Content-Type: text/html
Content-Length: 169
Connection: keep-alive
Location: https://site1.local/

rafael@Rafaels-MacBook-Pro html % curl -I https://site1.local
HTTP/1.1 502 Bad Gateway
Server: nginx/1.29.2
Date: Sun, 09 Nov 2025 21:31:43 GMT
Content-Type: text/html
Content-Length: 157
Connection: keep-alive

HTTP/1.1 301 Moved Permanently
Server: nginx/1.29.2
Date: Sun, 09 Nov 2025 21:32:36 GMT
Content-Type: text/html
Content-Length: 169
Connection: keep-alive
Location: https://site2.local/

rafael@Rafaels-MacBook-Pro html % curl -I https://site2.local
HTTP/1.1 200 OK
Server: nginx/1.29.2
Date: Sun, 09 Nov 2025 21:32:56 GMT
Content-Type: text/html
Content-Length: 22
Last-Modified: Thu, 06 Nov 2025 15:54:37 GMT
Connection: keep-alive
ETag: "690cc4bd-16"
Accept-Ranges: bytes

Тестирование:
Открываем локальную страницу site1.local:
rafael@Rafaels-MacBook-Pro html % open https://site1.local

site2.local - открывается таким же образом. Также можно открыть через адресную строку https://site2.local


Отчёт по лабораторной работе 1*: 
По началу я даже не знал с чего начать и что мне делать, и потому решил обратиться к ChatGPT. Естественно GPT отказался мне помогать взламывать сайты. После нескольких добрых и высоконравственных словосочетаний в адрес нейросети я попросил дать хотя бы инструменты для взлома site1.local. Ответом стал ffuf, так как это является простым и интересным способом (и действительно так). 

Для взлома возьмём сайт Факультета программной инженерии и компьютерной техники (https://se.ifmo.ru). 

ffuf -u https://se.ifmo.ru/FUZZ -w raft-small-words.txt -mc 200,302,403

rafael@Rafaels-MacBook-Pro Web-Content % ffuf -u https://se.ifmo.ru/FUZZ -w raft-small-words.txt -mc 200,302,403 
        /'___\  /'___\           /'___\       
       /\ \__/ /\ \__/  __  __  /\ \__/       
       \ \ ,__\\ \ ,__\/\ \/\ \ \ \ ,__\      
        \ \ \_/ \ \ \_/\ \ \_\ \ \ \ \_/      
         \ \_\   \ \_\  \ \____/  \ \_\       
          \/_/    \/_/   \/___/    \/_/       
       v2.1.0-dev
________________________________________________
 :: Method           : GET
 :: URL              : https://se.ifmo.ru/FUZZ
 :: Wordlist         : FUZZ: /Users/rafael/SecLists/Discovery/Web-Content/raft-small-words.txt
 :: Follow redirects : false
 :: Calibration      : false
 :: Timeout          : 10
 :: Threads          : 40
 :: Matcher          : Response status: 200,302,403
________________________________________________
installation            [Status: 200, Size: 0, Words: 1, Lines: 1, Duration: 45ms]
user                    [Status: 302, Size: 0, Words: 1, Lines: 1, Duration: 89ms]
forum                   [Status: 200, Size: 0, Words: 1, Lines: 1, Duration: 48ms]
cgi-bin                 [Status: 200, Size: 0, Words: 1, Lines: 1, Duration: 51ms]
search                  [Status: 200, Size: 47817, Words: 1966, Lines: 2841, Duration: 155ms]
download                [Status: 200, Size: 0, Words: 1, Lines: 1, Duration: 38ms]
profile                 [Status: 200, Size: 0, Words: 1, Lines: 1, Duration: 41ms]
bin                     [Status: 200, Size: 0, Words: 1, Lines: 1, Duration: 48ms]
category                [Status: 200, Size: 0, Words: 1, Lines: 1, Duration: 42ms]
password                [Status: 200, Size: 0, Words: 1, Lines: 1, Duration: 46ms]

…

pdf                     [Status: 200, Size: 0, Words: 1, Lines: 1, Duration: 65ms]
template                [Status: 200, Size: 0, Words: 1, Lines: 1, Duration: 60ms]
upload                  [Status: 200, Size: 0, Words: 1, Lines: 1, Duration: 81ms]
docs                    [Status: 200, Size: 0, Words: 1, Lines: 1, Duration: 131ms]
joinrequests            [Status: 200, Size: 0, Words: 1, Lines: 1, Duration: 125ms]
print                   [Status: 200, Size: 0, Words: 1, Lines: 1, Duration: 103ms]
db                      [Status: 302, Size: 0, Words: 1, Lines: 1, Duration: 46ms]
redirect                [Status: 200, Size: 0, Words: 1, Lines: 1, Duration: 58ms]
home                    [Status: 200, Size: 72595, Words: 5773, Lines: 3484, Duration: 80ms]
links                   [Status: 200, Size: 0, Words: 1, Lines: 1, Duration: 105ms]
mail                    [Status: 302, Size: 0, Words: 1, Lines: 1, Duration: 15ms]
tags                    [Status: 200, Size: 72689, Words: 5773, Lines: 3484, Duration: 91ms]
database                [Status: 200, Size: 0, Words: 1, Lines: 1, Duration: 85ms]

И так удалось зайти на внутренние страницы сайта. И когда я попытался зайти на какой-нибудь из доступных, сайт лёг. Мне до этого удалось зайти на несколько страниц (например https://se.ifmo.ru/21 или https://se.ifmo.ru/23 и в них была обнаружена старая дисциплина убранная из учебного плана, к которой больше нет доступа), но я не успел их сохранить. 



(Пришлось заходить через Windows, чтобы получить понятную ошибку)
При этом на этом сайте большое количество скрытых и старых страниц.

Поэтому я решил не ждать и пошёл на https://fitp.itmo.ru/. Очень интересно, что там можно найти). 

По итогу ничего интересного найдено не было. Все страницы перебрасывали либо на начальный экран, либо на авторизацию. Только https://fitp.itmo.ru/Profile ведёт в пустую страницу


Далее я решил взломать сайт КТ ИТМО: https://ct.itmo.ru/. Не знаю почему, но интересно попробовать, что там может быть. 
ffuf -u https://ct.itmo.ru/FUZZ -w raft-small-words.txt -mc 200,302,403

И в итоге получился огромный вывод, что проверять всё - сумасшествие. Но при попытке зайти на любую другую адресную строку из возможных - сайт встаёт. Но потом я обнаружил, что и этот сайт упал как se.ifmo. При повторном сканировании сканирование даже не начинается. 

Таким образом мне удалось взломать se.ifmo и получить большое количество пустых, старых и скрытых страниц, что нельзя получить обычным образом.



