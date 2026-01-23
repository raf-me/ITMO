package main.java;

import javax.swing.*;
import java.awt.*;

public class Facultets extends JFrame {
    private final CardLayout cards = new CardLayout();
    private final JPanel root = new JPanel(cards);

    public Facultets() {
        super("Выберите предмет");
        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setSize(1100, 850);
        setLocationRelativeTo(null);

        // Пример добавления экранов
        // root.add(createLabScreen(), "labs");

        add(root);
        cards.show(root, "labs");
        setVisible(true);
    }
}
