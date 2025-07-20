package org.example.Person;

import java.util.Scanner;

public class Name {
    private String name;

    public void NameManager() {
        Scanner sc = new Scanner(System.in);
        System.out.print("Введите имя: ");
        name = sc.nextLine();

        this.name = name;
    }
}
