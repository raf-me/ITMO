# Домашнее задание 1

## Лабораторная работа C#

Нужно разработать консольное приложение, которое будет эмулировать вендинговый автомат, позволяющее пользователю:

1. Посмотреть список доступных товаров с их ценами и количеством.  
2. Вставить монеты разных номиналов.  
3. Выбрать товар и получить его, если внесённой суммы достаточно.  
4. Получить сдачу (если нужно) и вернуть неиспользованные монеты при отмене операции.  
5. Администраторский режим для пополнения ассортимента и сбора собранных средств.  

*Решение необходимо загрузить ссылкой на свой открытый репозиторий GitHub* в ответе на assignment.  
Укажите также в assignment свой **Telegram**, **ФИО** и **группу**.

# Документация к реализации

**Директории проекта:**
Реализация программного продукта находится в:
ConsoleVendingMachine -> data -> [src](https://github.com/raf-me/ITMO/tree/main/FPIn/C%23/lab1/ConsoleVendingMachine/ConsoleVendingMachine/data/src/), [recources](https://github.com/raf-me/ITMO/tree/main/FPIn/C%23/lab1/ConsoleVendingMachine/ConsoleVendingMachine/data/recources)

В **[src](https://github.com/raf-me/ITMO/tree/main/FPIn/C%23/lab1/ConsoleVendingMachine/ConsoleVendingMachine/data/src/)** находится код программы
В **[recources](https://github.com/raf-me/ITMO/tree/main/FPIn/C%23/lab1/ConsoleVendingMachine/ConsoleVendingMachine/data/recources/)** находятся файлы, хранящие данные

1. src/[CollectionManager](https://github.com/raf-me/ITMO/tree/main/FPIn/C%23/lab1/ConsoleVendingMachine/ConsoleVendingMachine/data/src/CollectionManager/) - здесь хранится код, предназначенный для
чтения из **json**, формирования коллекций и функции внутренних технических операций автомата
[CollectionData](https://github.com/raf-me/ITMO/tree/main/FPIn/C%23/lab1/ConsoleVendingMachine/ConsoleVendingMachine/data/src/CollectionManager/CollectionData/) - здесь хранится классы:

**ReadJSONforAdmin.cs** - здесь мы читаем person.json из recources, в котором хранятся ключи для админов.

**ReadJSONforAssortmens.cs** - это класс распоковки данных из data.json, в котором хранятся данные ассортимента продуктов

[CollectionAdmin](https://github.com/raf-me/ITMO/tree/main/FPIn/C%23/lab1/ConsoleVendingMachine/ConsoleVendingMachine/data/src/CollectionManager/CollectionAdmin/) - здесь хранится классы:

**DataAdmin.cs** - здесь формируется коллекция, где во время выполнения программы в коллекции хрантся ключи для админов для "особого" пользования информационной системой.

**DataCollection.cs** - это, грубо говоря, мозг программы. Здесь реализованы такие методы, как: 

    AddProduct, RemoveProduct - Добавляют или удаляют продукт из коллекции; 
    
    PayProduct - обработка покупки с последующей обрабработкой коллекции, а также сохранение денег в кассе в money.json 
    
    SaveAssortment - сохранение коллекции в файл data.json
    
    PrintAllProduct - вывод доступных товаров из коллекции в консоль

**src/FunctionalManager** - директория, в которой содержатся две другие директории _AdminManager_ и _MoneyAndPayManager_:

1. **AdminManager** - Реализует классы:
  
       *AdminKey* - Реализует авторизацию админа по ключу, добавление товара в коллекцию (AddProduct),
           удаление товара из коллекции (RemoveProduct), и изъятие денег из кассы
   
       *NameProduct* - вводим имя
   
       *Price* - Ввод суммы товара
   
       *Quantity* - ввод количества товара
   
       *AssortmentAdmin* - Реализует методы AddManager и RemoveManager, которые вызывают классы
           _Price_, _Name_, _Quantity_, _DataCollection (методы AddProduct, RemoveProduct)_

3. **MoneyAndPayManager** - реализует классы:

   






