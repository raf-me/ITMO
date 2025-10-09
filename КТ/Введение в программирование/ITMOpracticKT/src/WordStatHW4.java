import java.util.*;
import java.util.Scanner;

public class WordStatHW4 {

    public void WordStat() {
        String[] Word = new String[]{
                "To be, or not to be, that is the question\n",
                "Monday's child is fair of face.\n" + "Tuesday's child is full of grace.\n",
                "Шалтай-Болтай\n" + "Сидел на стене.\n" + "Шалтай-Болтай\n" + "Свалился во сне."};

        System.out.println(Arrays.toString(Word));
        System.out.println("Выберите список");
        Scanner sc = new Scanner(System.in);
        String answer = sc.nextLine();

        int answerWord = Integer.parseInt(answer);
        answerWord = answerWord - 1;
        String test = Word[answerWord];
        System.out.println("Выбранный список: " + test);

        String[] statCheck = test.trim().split("\\s+");
        Map<String, Integer> Stat = new HashMap<>();

        for (String x : statCheck) {
            Stat.put(x, Stat.getOrDefault(x, 0) + 1);
        }

        for (Map.Entry<String, Integer> entry : Stat.entrySet()) {
            System.out.println(entry.getKey() + "->" + entry.getValue());
        }
    }
}
