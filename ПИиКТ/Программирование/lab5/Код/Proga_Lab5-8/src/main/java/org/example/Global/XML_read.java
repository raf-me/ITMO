package org.example.Global;

import org.w3c.dom.Document;
import org.w3c.dom.NodeList;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import java.io.IOException;
import java.io.InputStream;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;


public class XML_read {
    private String name; //Поле не может быть null, Строка не может быть пустой
    private String file_xml;


    public void Read_XML_Name() {
        try {
            InputStream inputStream = getClass().getClassLoader().getResourceAsStream("Person/Admin/Person.xml");

            DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
            DocumentBuilder builder = factory.newDocumentBuilder();
            Document doc = builder.parse(inputStream);

            doc.getDocumentElement().normalize();

            NodeList nameNodes = doc.getElementsByTagName("name");

            if (nameNodes.getLength() > 0) {
                String nameFromXml = nameNodes.item(0).getTextContent().trim();

                if (nameFromXml.isEmpty()) {
                    throw new IllegalArgumentException("Имя в XML не может быть пустым!");
                }

                this.name = nameFromXml;
                System.out.println("Имя профиля из XML: " + this.name);
            } else {
                throw new IllegalArgumentException("Тег <name> не найден в XML!");
            }

        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
