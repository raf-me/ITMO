package main.java.PIiKT1;

import javax.swing.*;
import java.awt.*;
import java.io.*;
import java.nio.charset.StandardCharsets;
import java.util.HashMap;

public class LabTemplateViewer extends JFrame {
    public LabTemplateViewer(String labName) {
        setTitle("Лабораторная работа: " + labName);
        setSize(800, 700);
        setDefaultCloseOperation(DISPOSE_ON_CLOSE);
        setLocationRelativeTo(null);

        JTextPane instructionPane = new JTextPane();
        instructionPane.setContentType("text/html");
        instructionPane.setEditable(false);
        instructionPane.setText(loadHtmlTemplate(labName));

        JScrollPane scroll = new JScrollPane(instructionPane);
        add(scroll, BorderLayout.CENTER);

        setVisible(true);
    }

    private String loadHtmlTemplate(String labName) {
        String path = "/ProgaLabNHTML/" + labName + ".html";
        try (InputStream in = getClass().getResourceAsStream(path)) {
            if (in == null) return "<html><body><p>Файл не найден: " + path + "</p></body></html>";
            return new String(in.readAllBytes(), StandardCharsets.UTF_8);
        } catch (IOException e) {
            return "<html><body><p>Ошибка загрузки файла: " + e.getMessage() + "</p></body></html>";
        }
    }
}
