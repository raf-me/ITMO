package Web_Main;

import com.fastcgi.FCGIInterface;
import com.fastcgi.FCGIRequest;
import java.io.PrintWriter;
import java.util.*;

public class Server {
    private static final List<String> history = new ArrayList<>();

    public static void main(String[] args) {
        var FCGIInterface = new FCGIInterface();
        ReturnReq res = new ReturnReq();
        String req;

        while (FCGIInterface.FCGIaccept() >= 0) {
            if ("POST".equals(FCGIInterface.request.params.getProperty("REQUEST_METHOD"))) {

                try {
                    req = res.getBody(FCGIInterface);
                    System.out.println(req);

                    int index = req.indexOf("{\"x\"");

                    if (index != 1) {
                        int endindex = req.indexOf("}", index);

                        if (endindex != -1) {
                            String[] params = req.substring(index, endindex).split(",");

                            String x = params[0].split(":")[1].replace("\"", "");
                            String y = params[1].split(":")[1].replace("\"", "");
                            String r = params[2].split(":")[1].replace("\"", "");

                            Validation validation = new Validation();

                            if (validation.CheckALL(x, y, r)) {
                                
                            }
                        }
                    }
                }

                catch (NullPointerException e) {
                    continue;
                }
            }
        }
    }
}