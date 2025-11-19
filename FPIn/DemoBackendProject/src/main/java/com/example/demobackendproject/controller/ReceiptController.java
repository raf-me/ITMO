package com.example.demobackendproject.controller;

import com.example.demobackendproject.dto.*;
import com.example.demobackendproject.service.HTTP_DATA;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import java.util.Map;

@RestController
@RequestMapping("/api")
public class ReceiptController {

    private final HTTP_DATA receiptService;

    public ReceiptController(HTTP_DATA receiptService) {
        this.receiptService = receiptService;
    }

    @PostMapping("/categorize")
    public ResponseEntity<ApiResponse<ReceiptDataDTO>> categorizeReceipt(
            @RequestBody ReceiptRequestDTO request) {

        ReceiptDataDTO data = receiptService.buildReceiptData(request);
        return ResponseEntity.ok(ApiResponse.ok(data));
    }

    @PostMapping("/decode")
    public ResponseEntity<String> decodeQR(@RequestBody ReceiptQRRequestDTO request) {
        String raw = receiptService.decodeRawQR(request.getQr());
        return ResponseEntity.ok(raw);
    }

    @PostMapping("/parseQr")
    public ResponseEntity<Map<String, String>> parseQr(@RequestBody ReceiptQrParseDTO request) {
        Map<String, String> parsed = receiptService.parseQrCode(request.getQr());
        return ResponseEntity.ok(parsed);
    }
}