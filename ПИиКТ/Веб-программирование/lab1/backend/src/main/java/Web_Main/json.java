package Web_Main;

import java.io.FileWriter;
import com.google.gson.Gson;

public class json {
    static class User {
        String name;
        float x;
        int y;
        float r;

        User(String name, float x, int y, float r) {
            this.name = name;
            this.x = x;
            this.y = y;
            this.r = r;
        }
    }

    public void htmlWriter() {
        // Создаем объект
        User user = new User("Alice", 25.0F, 3, 4.0F);

        // Преобразуем объект в JSON
        Gson gson = new Gson();
        String json = gson.toJson(user);

        // Записываем JSON в файл
        try (FileWriter file = new FileWriter("user" + user.name + ".json")) {
            file.write(json);
            System.out.println("Данные сохранены в user.json");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
