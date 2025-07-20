package org.example.StudyGroup;

import java.util.Scanner;

public class Semester {
    private int semester;
    private String answer;
    private int counter;


    public void SemesterCounter() {
        if (counter == 1) {
            semester = 1;
        }
    }

    public void SemesterNext() {
        if (counter == 2) {
            semester += 1;
        }
    }

    public void SemesterManager() {
        Scanner sc = new Scanner(System.in);
        System.out.print("Данные первокурсника? (Да, Нет): ");
        answer = sc.nextLine();

        if (answer == "Да" || answer == "да") {
            counter = 1;
            SemesterCounter();
            System.out.print("Данные обработаны!");
        }

        if (answer == "Нет" || answer == "нет") {
            counter = 2;
            SemesterNext();
            System.out.print("Данные обработаны!");
        }
    }
}
