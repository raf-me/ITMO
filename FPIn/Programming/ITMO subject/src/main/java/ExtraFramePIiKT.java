package main.java;

import main.java.PIiKT1.INFPIiKT;
import main.java.PIiKT1.ProgrammingCpp;
import main.java.PIiKT1.ProgrammingPIiKT;
import main.java.PIiKT1.Web;

import javax.swing.*;
import java.awt.*;

public class ExtraFramePIiKT extends JFrame {
    public ExtraFramePIiKT() {
        setTitle("ФПИиКТ");
        setSize(720, 800);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);

        JLabel label = new JLabel("С Возвращением!");
        JLabel talk1 = new JLabel(" ");
        JButton ProgrammingPIiKT = new JButton("Программирование");
        JButton INFPIiKT = new JButton("Информатика");
        JButton ButtonINF = new JButton("Информатика");
        JButton DataBase = new JButton("Базы данных");
        JButton OPJ = new JButton("Основы профессиональной деятельности");
        JButton Web = new JButton("Веб-программирование");
        JButton BLPS = new JButton("Бизнес-логика программных систем");
        JButton OPI = new JButton("Основы программной инженерии");
        JButton ASD = new JButton("Алгоритмы и Структуры данных");
        JButton Cpp = new JButton("Программирование на C++");
        JButton backButton = new JButton("Назад в окно пользователя");



        JButton[] buttons = { ProgrammingPIiKT, INFPIiKT, DataBase, OPJ, Web, BLPS, OPI, ASD, Cpp};


        ProgrammingPIiKT.addActionListener(e -> { new ProgrammingPIiKT(); dispose(); });
        INFPIiKT.addActionListener(e -> { new INFPIiKT(); dispose(); });
        Web.addActionListener(e -> { new Web(); dispose(); });
        Cpp.addActionListener(e -> {new ProgrammingCpp(); dispose();});
        //DataBase  .addActionListener(e -> { new DataBase();  dispose(); });
        //OPJ  .addActionListener(e -> { new OPJ();  dispose(); });



        JPanel top = new JPanel(new BorderLayout());
        //JButton backButton = new JButton("< Назад");
        backButton.addActionListener(e -> {
            new UserFrame();
            dispose();
        });
        JLabel title = new JLabel("Дисциплины КСиТ", SwingConstants.CENTER);
        title.setFont(new Font("Arial", Font.PLAIN, 24));
        top.add(backButton, BorderLayout.WEST);
        top.add(title,      BorderLayout.CENTER);


        JPanel table = new JPanel(new GridLayout(0, 2, 8, 8));
        table.setBorder(BorderFactory.createEmptyBorder(12,12,12,12));


        for (JButton btn : buttons) {
            table.add(btn);
            JTextField comment = new JTextField();
            comment.setEnabled(AccessManager.canComment());
            table.add(comment);
        }


        setLayout(new BorderLayout());
        add(top,    BorderLayout.NORTH);
        add(new JScrollPane(table), BorderLayout.CENTER);

        setVisible(true);
    }
}

