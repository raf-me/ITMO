package com.example.demobackendproject.api;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Component;
import org.springframework.web.reactive.function.client.WebClient;
import reactor.core.publisher.Mono;

@Component
public class ReceiptDecodeApi {

    private WebClient webClient;

    @Value("${receipt.api.token}")
    private String apiToken;

    @Value("${receipt.api.url}")
    private String apiUrl;

    public ReceiptDecodeApi(WebClient webClient) {
        this.webClient = webClient;
    }

    public Mono<String> decodeReceipt(String receiptId) {

        if (apiToken == null || apiToken.isBlank() || "token".equalsIgnoreCase(apiToken)) {
            return Mono.just("{\"mock\":true,\"message\":\"external call skipped\"}");
        }

        return webClient.get()
                .uri(uriBuilder -> uriBuilder.path(apiUrl).queryParam("token", apiToken)
                        .queryParam("qr", receiptId)
                        .build())
                .exchangeToMono(resp -> {
                    if (resp.statusCode().is2xxSuccessful()) {
                        return resp.bodyToMono(String.class);
                    } else {
                        return resp.bodyToMono(String.class)
                                .defaultIfEmpty("")
                                .flatMap(body -> Mono.error(
                                        new ReceiptApiException((HttpStatus) resp.statusCode(), body)));
                    }
                });
    }

    public class ReceiptApiException extends RuntimeException {
        private final HttpStatus status;
        private final String body;
        public ReceiptApiException(HttpStatus status, String body) {
            super("Receipt API error " + status.value() + ": " + body);
            this.status = status; this.body = body;
        }
        public HttpStatus getStatus() { return status; }
        public String getBody() { return body; }
    }
}