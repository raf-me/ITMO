package org.example.Coordinates;

public class Coordinates {
    private Long x; //Поле не может быть null
    private Double y; //Максимальное значение поля: 95, Поле не может быть null

    public Coordinates(Long x, Double y) {
        if (x == null || y == null || y > 95) {
            throw new IllegalArgumentException("Некорректные координаты!");
        }

        this.x = x;
        this.y = y;
    }
    public Long getX() {
        return x;
    }

    public Double getY() {
        return y;
    }

    @Override
    public String toString() {
        return "(" + x + ", " + y + ")";
    }
}
