import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.*;

public class HW5_ScannerReader {
    String NewScanner;

    public String TestScannerReader() {
        try {
            InputStreamReader reader = new InputStreamReader(System.in);
            BufferedReader bf = new BufferedReader(reader);
            System.out.println("Введите что-нибудь");
            NewScanner = bf.readLine();

            System.out.println("Вы ввели - " + NewScanner);
            ManagerForNewHW3and4();

        } catch (IOException e) {
            throw new RuntimeException(e);
        }
        return null;
    }

    public String ScannerReader() {
        try {
            InputStreamReader reader = new InputStreamReader(System.in);
            BufferedReader bf = new BufferedReader(reader);
            NewScanner = bf.readLine();

        } catch (IOException e) {
            throw new RuntimeException(e);
        }

        return null;
    }

    public void ManagerForNewHW3and4() throws IOException {
        InputStreamReader reader = new InputStreamReader(System.in);
        BufferedReader bf = new BufferedReader(reader);
        System.out.println("Проверить метод на 3 или 4 ДЗ?");
        NewScanner = bf.readLine();

        if (NewScanner.equals("да")) {
            NewReversForHW3();
            NewWordStatForHW4();
        }
    }

    public void NewReversForHW3() {
        String[] listTest = new String[]{"1 2 3", "1 3 2", "1 5 3", "1 5 3 7"};
        System.out.println(Arrays.toString(listTest));
        System.out.println("Выбери список: ");

        String answer = ScannerReader();

        int answerTest = Integer.parseInt(answer.trim());
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

    public void NewWordStatForHW4() {
        String[] Word = new String[]{
                "To be, or not to be, that is the question\n",
                "Monday's child is fair of face.\n" + "Tuesday's child is full of grace.\n",
                "Шалтай-Болтай\n" + "Сидел на стене.\n" + "Шалтай-Болтай\n" + "Свалился во сне."};

        System.out.println(Arrays.toString(Word));
        System.out.println("Выберите список");
        String answer = ScannerReader();

        int answerWord = Integer.parseInt(answer.trim());
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
