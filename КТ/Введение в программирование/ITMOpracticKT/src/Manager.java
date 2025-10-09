import java.util.Scanner;

public class Manager {
    private String manager;

    public void HomeWorkManager() {
        System.out.println("Выберите домашнюю работу (exit, чтобы выйти):");

        Scanner sc = new Scanner(System.in);
        manager = sc.nextLine();

        if (manager.equals("1")) {
            System.out.println("HW1");
            HW1 hw1 = new HW1();
            hw1.homeWork1();
            HomeWorkManager();
        }

        if (manager.equals("2")) {
            HW2 hw2 = new HW2();
            hw2.SUM2();
            HomeWorkManager();
        }

        if (manager.equals("3")) {
            HW3 hw3 = new HW3();
            hw3.Revers();
            HomeWorkManager();
        }

        if (manager.equals("4")) {
            WordStatHW4 wordStatHW4 = new WordStatHW4();
            wordStatHW4.WordStat();
            HomeWorkManager();
        }

        if (manager.equals("5")) {
            HW5_ScannerReader hw5ScannerReader = new HW5_ScannerReader();
            hw5ScannerReader.TestScannerReader();
            HomeWorkManager();
        }

        if (manager.equals("exit")) {
            System.exit(0);
        }
    }
}
