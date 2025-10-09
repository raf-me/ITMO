import java.util.Scanner;

public class HW1 {
    private static String answer;

    public void homeWork1() {
        System.out.println("Hello, World!");

        System.out.println("Поприветствуем информацию? (Yes or No)");

        Scanner sc = new Scanner(System.in);
        answer = sc.nextLine();
        if (answer.equals("Yes")) { System.out.println("Hello, prog-intro!"); }
        if (!answer.equals("No") && !answer.equals("Yes")) {System.out.println("Error!");}
    }
}
