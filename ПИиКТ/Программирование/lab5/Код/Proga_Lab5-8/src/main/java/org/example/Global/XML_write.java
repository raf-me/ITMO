package org.example.Global;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

//import org.example.StudyGroup.Group_Name.Name_write.input;

public class XML_write {
    private String name;

    public void Write_XLM(String file_xml) throws IOException {
        try {
            String filepath = "src/main/resources/Person/Client/Person" + name + ".xml";
            Path path = Paths.get(file_xml);
            Files.createFile(path);



        }

        catch (Exception e) {
            System.err.println("Ошибка: " + e.getMessage());
        }
    }
}
