import java.util.*;
import java.util.Scanner;

public class HW3 {
    public void Revers() {
        String[] listTest = new String[]{"1 2 3", "1 3 2", "1 5 3", "1 5 3 7"};
        System.out.println(Arrays.toString(listTest));
        System.out.println("Выбери список");
        Scanner sc = new Scanner(System.in);
        String answer = sc.nextLine();
        int answerTest = Integer.parseInt(answer);
        answerTest = answerTest - 1;

        String test = listTest[answerTest];
        System.out.println("Выбранный список: " + test);

        String[] parts = test.trim().split("\\s+");
        List<String> listReverse = new ArrayList<>();

        for (int i = parts.length - 1; i >= 0; i--) {
            listReverse.add(parts[i]);
            System.out.print(parts[i] + " ");
        }

        System.out.println(listReverse);
    }
}
