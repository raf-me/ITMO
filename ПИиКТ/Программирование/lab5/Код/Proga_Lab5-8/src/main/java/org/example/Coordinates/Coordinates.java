package org.example.Coordinates;

import java.util.Scanner;

public class Coordinates {
    private Long x; //Поле не может быть null
    private Double y; //Максимальное значение поля: 95, Поле не может быть null

    public Long getX() {
        Scanner sc = new Scanner(System.in);
        System.out.print("Введите ваши координаты x: ");
        x = sc.nextLong();
        return x;
    }

    public Double getY() {
        Scanner sc = new Scanner(System.in);
        System.out.print("Введите ваши координаты y: ");
        y = sc.nextDouble();
        return y;
    }

    public Coordinates(Long x, Double y) {
        getX();
        getY();

        if (x == null || y == null || y > 95) {
            throw new IllegalArgumentException("Некорректные координаты!");
        }

        this.x = x;
        this.y = y;
    }

    @Override
    public String toString() {
        return "(" + x + ", " + y + ")";
    }
}
