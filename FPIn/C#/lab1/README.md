# Домашнее задание 1

## Лабораторная работа C#

Нужно разработать консольное приложение, которое будет эмулировать вендинговый автомат, позволяющее пользователю:

1. Посмотреть список доступных товаров с их ценами и количеством.  
2. Вставить монеты разных номиналов.  
3. Выбрать товар и получить его, если внесённой суммы достаточно.  
4. Получить сдачу (если нужно) и вернуть неиспользованные монеты при отмене операции.  
5. Администраторский режим для пополнения ассортимента и сбора собранных средств.  

*Решение необходимо загрузить ссылкой на свой открытый репозиторий GitHub** в ответе на assignment.  
Укажите также в assignment свой **Telegram**, **ФИО** и **группу**.

# Документация к реализации

**Директории проекта:**
Реализация программного продукта находится в:
ConsoleVendingMachine -> data -> [src](https://github.com/raf-me/ITMO/tree/main/FPIn/C%23/lab1/ConsoleVendingMachine/ConsoleVendingMachine/data/src/), [recources](https://github.com/raf-me/ITMO/tree/main/FPIn/C%23/lab1/ConsoleVendingMachine/ConsoleVendingMachine/data/recources)

В **[src](https://github.com/raf-me/ITMO/tree/main/FPIn/C%23/lab1/ConsoleVendingMachine/ConsoleVendingMachine/data/src/)** находится код программы
В **[recources](https://github.com/raf-me/ITMO/tree/main/FPIn/C%23/lab1/ConsoleVendingMachine/ConsoleVendingMachine/data/recources/)** находятся файлы, хранящие данные

**[src](https://github.com/raf-me/ITMO/tree/main/FPIn/C%23/lab1/ConsoleVendingMachine/ConsoleVendingMachine/data/src/)**: 
[CollectionManager](https://github.com/raf-me/ITMO/tree/main/FPIn/C%23/lab1/ConsoleVendingMachine/ConsoleVendingMachine/data/src/CollectionManager/) - здесь хранится код, предназначенный для
чтения из **json**, формирования коллекций и функции внутренних технических операций автомата
