package org.example.StudyGroup;

import java.util.ArrayList;
import java.util.List;
import org.example.Person.*;

public class Group_ID extends ID_Generator {
    private long id; //Значение поля должно быть больше 0, Значение этого поля должно быть уникальным, Значение этого поля должно генерироваться автоматически
    protected long id_client;
    protected long id_admin = 0;
    protected long id_root;
    private Person groupAdmin;


    public Group_ID(long id) {
        List<Long> ID_xml_read = new ArrayList<Long>();

        if (id <= 0) {
            throw new IllegalArgumentException("Неверный ID!");
        }

        this.id = id;
    }

    public void Group_Admin() {
        if (id_admin == 0){

        }
        /* if (id_admin == "1" + toString(id)) */ {

        }
    }

    public void Profile_ID() {

    }

    public void Access_ID() {

    }

    public void Ban_ID() {

    }

    public void Individual_edition_ID() {

    }
}
