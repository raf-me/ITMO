package Web_Main;

import com.fastcgi.FCGIInterface;

import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.nio.charset.StandardCharsets;

public class ReturnReq {
    public String getBody (FCGIInterface fcgiInterface) {
        try {
            InputStream input = fcgiInterface.request.inStream;
            ByteArrayOutputStream buffer = new ByteArrayOutputStream();

            byte[] data = new byte[1024];
            int read;

            while ((read = input.read(data)) != -1) {
                buffer.write(data, 0, read);
            }

            return buffer.toString(StandardCharsets.UTF_8);
        }

        catch (Exception e) {
            return "Ошибка чтения тела запроса: " + e.getClass().getSimpleName();
        }

    }
}
