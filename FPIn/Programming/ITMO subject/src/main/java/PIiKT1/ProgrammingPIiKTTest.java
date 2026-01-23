package main.java.PIiKT1;

import org.junit.jupiter.api.*;
import javax.swing.*;
import java.io.File;
import static org.junit.jupiter.api.Assertions.*;

public class ProgrammingPIiKTTest {

    private ProgrammingPIiKT window;

    @BeforeEach
    public void setup() {
        SwingUtilities.invokeLater(() -> window = new ProgrammingPIiKT());
    }

    @AfterEach
    public void tearDown() {
        if (window != null) {
            window.dispose();
        }
    }

    @Test
    public void testWindowTitle() {
        assertEquals("Программирование 1 курс", window.getTitle());
    }

    @Test
    public void testWindowIsVisible() {
        assertTrue(window.isVisible());
    }

    @Test
    public void testAddTaskButtonExists() {
        boolean found = false;
        for (java.awt.Component c : window.getContentPane().getComponents()) {
            if (c instanceof JScrollPane scrollPane) {
                java.awt.Component view = scrollPane.getViewport().getView();
                if (view instanceof JPanel panel) {
                    for (java.awt.Component child : panel.getComponents()) {
                        if (child instanceof JButton btn && btn.getText().contains("Добавить работу")) {
                            found = true;
                            break;
                        }
                    }
                }
            }
        }
        assertTrue(found, "Кнопка 'Добавить работу' не найдена");
    }

    @Test
    public void testGetNextLabNumberCreatesDirectoryIfMissing() {
        File dir = new File("src/main/resources/ProgaLabNHTML");
        if (dir.exists()) {
            for (File f : dir.listFiles()) {
                f.delete();
            }
            dir.delete();
        }
        ProgrammingPIiKT piikt = new ProgrammingPIiKT();
        int number = piikt.getNextLabNumber();
        assertEquals(1, number);
    }

    //ручной запуск
    public static void main(String[] args) {
        SwingUtilities.invokeLater(() -> new ProgrammingPIiKT());
    }
}
