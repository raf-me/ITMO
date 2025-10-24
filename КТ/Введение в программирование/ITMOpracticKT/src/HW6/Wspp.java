package HW6;
import java.io.*;
import java.util.*;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.concurrent.atomic.AtomicInteger;

public class Wspp {
    private String write;
    private int check;
    private String nextIteration;

    private static final AtomicInteger CHECK_COUNTER = new AtomicInteger(0);
    private static final DateTimeFormatter TS_FMT =
            DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss.SSS");

    private static long usedMemoryBytes() {
        Runtime rt = Runtime.getRuntime();
        return rt.totalMemory() - rt.freeMemory();
    }

    private static void simulateServerCall() {
        try { Thread.sleep(150); } catch (InterruptedException ignored) {}
    }

    private static void appendCheckToHistory(int checkId, LocalDateTime ts, long elapsedNanos, long memDeltaBytes) {
        String report =
                "==== CHECK #" + checkId + " ====\n" +
                        "Дата/время запроса: " + ts.format(TS_FMT) + "\n" +
                        "Номер запроса: " + checkId + "\n" +
                        "Время выполнения: " + (elapsedNanos / 1_000_000) + " ms\n" +
                        "Исп. память (дельта): " + String.format("%,d", memDeltaBytes) + " bytes\n" +
                        "======================\n";
        
        System.out.print(report);
        
        try (BufferedWriter bw = new BufferedWriter(new FileWriter("history.txt", true))) {
            bw.write(report);
            bw.newLine();
            
        } catch (IOException e) {
            System.err.println("Ошибка записи отчёта в history.txt: " + e.getMessage());
        }
    }

    public void NewClassForCollection() throws IOException {
        int check = CHECK_COUNTER.incrementAndGet();
        LocalDateTime ts = LocalDateTime.now();
        simulateServerCall();

        long t0 = System.nanoTime();
        long mem0 = usedMemoryBytes();

        String fileName = "src/HW6/in.txt";
        BufferedReader bf = new BufferedReader(new FileReader(fileName));
        Map<Integer, String> writeLine = new LinkedHashMap<>();

        try {
            int count = 0;
            String line;
            while ((line = bf.readLine()) != null) {
                writeLine.put(count, line);
                count += 1;
            }
        } finally {
            bf.close();
        }

        System.out.println("Выберите один из вариантов выполнения: \n" +
                "(1. Введите вами строку; 2. Выберите из предложенных) ");
        Scanner sc = new Scanner(System.in);
        write = sc.nextLine();

        if (write.equals("1")) {
            System.out.println("Введите строку: ");
            Scanner sc1 = new Scanner(System.in);
            String write1 = sc1.nextLine();

            Parse(write1);

            long elapsed = System.nanoTime() - t0;
            long memDelta = Math.max(0, usedMemoryBytes() - mem0);
            appendCheckToHistory(check, ts, elapsed, memDelta);
        }

        if (write.equals("2")) {
            System.out.println(writeLine);
            System.out.println("Выберите строку: ");
            Scanner sc2 = new Scanner(System.in);
            write = sc2.nextLine();

            try {
                Integer.parseInt(write);
            } catch (NumberFormatException error) {
                System.out.println("Ошибка ввода!");
                System.exit(0);
            }

            int writeCount = Integer.parseInt(write);
            writeCount = writeCount - 1;
            String nextIteration = writeLine.get(writeCount);
            HistoryWrite(nextIteration, check, ts, t0, mem0);
        }
    }

    public void HistoryWrite(String writeFile, int check, LocalDateTime ts, long t0, long mem0) {
        writeFile = writeFile.replaceAll("[^\\p{L}\\s]", "").toLowerCase();

        String[] statCheck = writeFile.trim().isEmpty() ? new String[0] : writeFile.trim().split("\\s+");
        Map<String, List<Integer>> Collect = new LinkedHashMap<>();
        int i = 1;

        for (String x : statCheck) {
            List<Integer> list = Collect.getOrDefault(x, new ArrayList<>());
            list.add(i);
            Collect.put(x, list);
            i++;
        }

        try (BufferedWriter bw = new BufferedWriter(new FileWriter("src/HW6/history.txt", true))){
            bw.write("======================");
            bw.newLine();

            for (Map.Entry<String, List<Integer>> entry : Collect.entrySet()) {
                String word = entry.getKey();
                List<Integer> list = entry.getValue();
                int count = list.size();

                bw.write(word + " " + count);
                for (int pos : list) {
                    bw.write(" " + pos);
                }
                bw.newLine();
            }

            bw.write("----------------");
            bw.newLine();

            long elapsed = System.nanoTime() - t0;
            long memDelta = Math.max(0, usedMemoryBytes() - mem0);
            String report =
                    "==== CHECK #" + check + " ====\n" +
                            "Дата/время запроса: " + ts.format(TS_FMT) + "\n" +
                            "Номер запроса: " + check + "\n" +
                            "Время выполнения: " + (elapsed / 1_000_000) + " ms\n" +
                            "Исп. память (дельта): " + String.format("%,d", memDelta) + " bytes\n" +
                            "======================\n";
            
            bw.write(report);
            bw.newLine();

            System.out.print(report);
        } catch (IOException e) {
            System.err.println("Ошибка записи в history.txt: " + e.getMessage());
        }
    }

    public void Parse(String write){
        write = write.replaceAll("[^\\p{L}\\s]", "").toLowerCase();

        String[] statCheck = write.trim().isEmpty() ? new String[0] : write.trim().split("\\s+");
        Map<String, List<Integer>> Collect = new LinkedHashMap<>();
        int i = 1;

        for (String x : statCheck) {
            List<Integer> list = Collect.getOrDefault(x, new ArrayList<>());
            list.add(i);
            Collect.put(x, list);
            i++;
        }

        System.out.println("----------------");
        for (Map.Entry<String, List<Integer>> entry : Collect.entrySet()) {
            String word = entry.getKey();
            List<Integer> list = entry.getValue();
            System.out.print(word + " " + list.size());
            for (int pos : list) {
                System.out.print(" " + pos);
            }
            System.out.println();
        }
        System.out.println("----------------");
    }
}
