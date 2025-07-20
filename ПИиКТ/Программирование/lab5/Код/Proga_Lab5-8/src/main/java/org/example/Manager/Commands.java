package org.example.Manager;

//import static sun.jvm.hotspot.runtime.BasicObjectLock.size;
import org.example.Global.XML_write;
import org.example.StudyGroup.Group_ID;
import org.example.StudyGroup.*;

public class Commands {
    public static String command;

    public void help() {
        // help: вывести справку по доступным командам
        if (command == "help") {
            System.out.println("");

        }
    }

    public void info() {
        // info: вывести в стандартный поток вывода информацию о коллекции (тип, дата инициализации, количество элементов и т.д.)

        for () {

        }
    }

    public void show() {
        // show: вывести в стандартный поток вывода все элементы коллекции в строковом представлении

        if ((id == id_admin && id == id_root) || command == "show") {
            for (int i = 0, client.size(), i++) {
                System.out.println(id, Group_ID, Password_name);
            }
        }
    }

    public void insert_null() {
        // insert null {element} : добавить новый элемент с заданным ключом
    }

    public void update_id() {
        // update id {element} : обновить значение элемента коллекции, id которого равен заданному
    }

    public void clear() {
        // clear : очистить коллекцию

    }

    public void save() {
        // save : сохранить коллекцию в файл
    }
}
