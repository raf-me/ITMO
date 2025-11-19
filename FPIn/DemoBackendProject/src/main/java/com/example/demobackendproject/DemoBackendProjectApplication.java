package com.example.demobackendproject;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication(scanBasePackages = "com.example.demobackendproject")
public class DemoBackendProjectApplication {

    public static void main(String[] args) {
        SpringApplication.run(DemoBackendProjectApplication.class, args);
    }
}
