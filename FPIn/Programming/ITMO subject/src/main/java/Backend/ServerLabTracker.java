package main.java.Backend;

import com.fastcgi.FCGIInterface;
import com.fastcgi.FCGIRequest;
import java.io.PrintWriter;
import java.util.*;
import java.util.ArrayList;
import java.util.List;

public class ServerLabTracker {
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



                } catch (RuntimeException e) {
                    throw new RuntimeException(e);
                }
            }
        }
    }
}
