package org.example.Person;

import java.util.Scanner;

public class PassportID {
    private int passportID;

    public void ManagerPassportID() {
        Scanner sc = new Scanner(System.in);
        System.out.print("Введите паспортные данные (6 цифр): ");
        passportID = sc.nextInt();

        this.passportID = passportID;
    }
}
