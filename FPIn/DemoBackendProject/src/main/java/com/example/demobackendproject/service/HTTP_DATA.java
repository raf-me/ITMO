package com.example.demobackendproject.service;

import com.example.demobackendproject.api.ReceiptDecodeApi;
import com.example.demobackendproject.dto.*;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;
import org.springframework.web.reactive.function.client.WebClientResponseException;
import org.springframework.web.server.ResponseStatusException;

import java.time.Duration;
import java.time.LocalDateTime;
import java.util.*;

@Service
public class HTTP_DATA {

    private final ReceiptDecodeApi decodeApi;

    public HTTP_DATA(ReceiptDecodeApi decodeApi) {
        this.decodeApi = decodeApi;
    }

    public String decodeRawQR(String qrString) {
        try {
            return decodeApi.decodeReceipt(qrString).block(Duration.ofSeconds(10));
        } catch (WebClientResponseException ex) {
            throw new ResponseStatusException(
                    HttpStatus.BAD_GATEWAY,
                    "Decode API returned " + ex.getStatusCode().value() + ": " + ex.getResponseBodyAsString(),
                    ex
            );
        } catch (Exception ex) {
            throw new ResponseStatusException(
                    HttpStatus.SERVICE_UNAVAILABLE,
                    "Decode API call failed: " + ex.getMessage(),
                    ex
            );
        }
    }

    public ReceiptDataDTO buildReceiptData(ReceiptRequestDTO request) {
        ReceiptDataDTO data = new ReceiptDataDTO();

        try {
            data.setId(Long.parseLong(
                    Optional.ofNullable(request.getReceiptId()).orElse("0")));
        } catch (NumberFormatException ignore) {
            data.setId(0);
        }

        data.setUser("...");

        CategoryDTO cat = findOrDefaultCategory(request.getCategories());
        ReceiptItemDTO item = new ReceiptItemDTO();
        item.setId(1);
        item.setName("Товар");
        item.setPrice(100d);
        item.setQuantity(1d);
        item.setSum(100d);
        item.setCategory(cat);

        List<ReceiptItemDTO> items = new ArrayList<>();
        items.add(item);
        data.setItems(items);

        double total = items.stream()
                .map(ReceiptItemDTO::getSum)
                .filter(Objects::nonNull)
                .mapToDouble(Double::doubleValue)
                .sum();
        data.setTotalSum(total);

        data.setDateTime(LocalDateTime.parse("2024-01-01T12:00:00"));

        return data;
    }

    private CategoryDTO findOrDefaultCategory(List<CategoryDTO> fromRequest) {
        if (fromRequest != null) {
            for (CategoryDTO c : fromRequest) {
                if (c != null && Objects.equals(c.getId(), 1)) {
                    return c;
                }
            }
        }

        CategoryDTO def = new CategoryDTO();
        def.setId(1);
        def.setName("Продукты");
        def.setDescription("Продукты питания");
        return def;
    }

    public Map<String, String> parseQrCode(String qr) {
        Map<String, String> map = new HashMap<>();
        if (qr == null || qr.isEmpty()) return map;

        for (String part : qr.split("&")) {
            String[] kv = part.split("=");

            if (kv.length == 2) map.put(kv[0], kv[1]);
        }

        return map;
    }
}