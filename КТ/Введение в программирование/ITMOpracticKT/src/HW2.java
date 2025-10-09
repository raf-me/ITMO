import java.util.*;

public class HW2 {
    public void SUM2() {
        //java Sum ...
        Scanner sc = new Scanner(System.in);
        String num = sc.nextLine();

        if (num.contains("java Sum ")){
            num = num.replace("java Sum ", "");

            long count = num.chars().filter(ch -> ch == '"').count();
            if (count > 0) {
                num = num.replace("\"", "");
            }

            if (num.equals(" ")) {
                System.out.println("0");
                System.exit(0);
            }

            String[] sum = num.trim().split("\\s+");
            int sumEnd = 0, sumNext;
            for (int i = sum.length - 1; i >= 0; i--) {
                sumNext = Integer.parseInt(sum[i]);
                sumEnd += sumNext;
            }
            System.out.println(sumEnd);
        }
    }
}
