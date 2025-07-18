package org.example.StudyGroup;

import java.util.Scanner;

public class Group_Name {
    private String name; //Поле не может быть null, Строка не может быть пустой
    private String name_write;

    public Group_Name() {
        if ((name == null) || (name == "")) {
            throw new IllegalArgumentException("Неверный формат имени!");
        }

        this.name = name;
    }

    public void Name_write() {

        System.out.println("Введите имя: ");
        Scanner sc = new Scanner(System.in);

        String input = sc.nextLine();

        if (input == null || input.trim().isEmpty()) {
            throw new IllegalArgumentException("Имя не может быть пустым!");
        }

        else if ((name_write == null) || (name_write == "")) {
            throw new IllegalArgumentException("Неверный формат имени!");
        }

        else {
            this.name_write = input;
            name = name_write;
        }

    }

}
