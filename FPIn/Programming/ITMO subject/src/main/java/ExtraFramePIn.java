package main.java;

import main.java.PIn.ASDPIn;
import main.java.PIn.INFPIn;
import main.java.PIn.ProgrammingPIn;
import main.java.PIn.WebPIn;

import javax.swing.*;
import java.awt.*;

public class ExtraFramePIn extends JFrame {
    public ExtraFramePIn() {
        setTitle("ФПИн");
        setSize(720, 800);
        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setLocationRelativeTo(null);

        JButton programmingBtn = new JButton("Программирование");
        JButton informaticsBtn = new JButton("Информатика");
        JButton webBtn = new JButton("Веб-программирование");
        JButton algorithmsBtn = new JButton("Алгоритмы и Структуры данных");
        JButton backButton = new JButton("Назад в окно пользователя");

        JButton[] buttons = { programmingBtn, informaticsBtn, webBtn, algorithmsBtn };

        programmingBtn.addActionListener(e -> { new ProgrammingPIn(); dispose(); });
        informaticsBtn.addActionListener(e -> { new INFPIn(); dispose(); });
        webBtn.addActionListener(e -> { new WebPIn(); dispose(); });
        algorithmsBtn.addActionListener(e -> { new ASDPIn(); dispose(); });
        backButton.addActionListener(e -> { new UserFrame(); dispose(); });

        JPanel top = new JPanel(new BorderLayout());
        JLabel title = new JLabel("Дисциплины МиСТ", SwingConstants.CENTER);
        title.setFont(new Font("Arial", Font.PLAIN, 24));
        top.add(backButton, BorderLayout.WEST);
        top.add(title, BorderLayout.CENTER);

        JPanel table = new JPanel(new GridLayout(0, 2, 8, 8));
        table.setBorder(BorderFactory.createEmptyBorder(12, 12, 12, 12));

        for (JButton btn : buttons) {
            table.add(btn);
            JTextField comment = new JTextField();
            comment.setEnabled(AccessManager.canComment());
            table.add(comment);
        }

        setLayout(new BorderLayout());
        add(top, BorderLayout.NORTH);
        add(new JScrollPane(table), BorderLayout.CENTER);

        setVisible(true);
    }
}
