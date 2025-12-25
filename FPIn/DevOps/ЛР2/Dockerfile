FROM openjdk:latest

WORKDIR /app

COPY . .

RUN mvn clean package

EXPOSE 8080

CMD ["java", "-jar", "target/DemoBackendProject-0.0.1-SNAPSHOT.jar"]