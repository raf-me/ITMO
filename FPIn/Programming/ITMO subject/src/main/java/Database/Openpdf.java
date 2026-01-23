/*
package main.resources.java.studenthelper.java.PIiKT1.Database;

import java.awt.Desktop;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.nio.file.Files;
import java.nio.file.StandardCopyOption;

public class Openpdf {
    public static void main(String[] args) {
        // Имя файла в ресурсах
        String pdfFileName = "src\\main.resources.java.studenthelper\\progaЛабораторнаяработа3-4.pdf";

        try {
            // Получаем InputStream для ресурса
            InputStream inputStream = Openpdf.class
                    .getClassLoader()
                    .getResourceAsStream(pdfFileName);

            if (inputStream == null) {
                System.err.println("Файл не найден в ресурсах: " + pdfFileName);
                return;
            }

            // Создаем временный файл
            File tempFile = File.createTempFile("temp_pdf", ".pdf");
            tempFile.deleteOnExit(); // Удалить при завершении программы

            // Копируем данные из InputStream во временный файл
            Files.copy(inputStream, tempFile.toPath(), StandardCopyOption.REPLACE_EXISTING);

            // Открываем PDF с помощью Desktop
            if (Desktop.isDesktopSupported()) {
                Desktop desktop = Desktop.getDesktop();
                if (tempFile.exists()) {
                    desktop.open(tempFile);
                }
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
*/