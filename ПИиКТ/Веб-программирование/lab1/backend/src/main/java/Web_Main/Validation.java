package Web_Main;
import com.fastcgi.*;

public class Validation {

    public boolean CheckX(String x) {
        try {
            double valve = Double.parseDouble(x);
            if (valve >= -2 && valve <= 2) {
                return true;
            }

            else {
                return false;
            }

        } catch (NumberFormatException e) {
            return false;
        }
    }

    public boolean CheckY(String y) {
        try {
            double valve = Double.parseDouble(y);

            if (valve > -3 && valve < 5) {
                return true;
            }

            else {
                return false;
            }

        } catch (NumberFormatException e) {
            return false;
        }
    }

    public boolean CheckR(String r) {
        try {
            double valve = Double.parseDouble(r);

            if (valve >= 1 && valve <= 3) {
                return true;
            }

            else { return false; }
        } catch (NumberFormatException e) {
            return false;
        }
    }

    public boolean CheckALL(String x, String y, String r) {
        return (CheckX(x) && CheckY(y) && CheckR(r));
    }
}
