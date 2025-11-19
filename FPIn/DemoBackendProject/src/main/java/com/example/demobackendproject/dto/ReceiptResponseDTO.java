package com.example.demobackendproject.dto;

import java.util.List;
import java.util.Map;

public class ReceiptResponseDTO {

    private String receiptId;
    private List<ReceiptItemDTO> items;
    private float totalSum;
    private List<CategoryDTO> categories;
    private Map<String, Object> debugData;

    public String getReceiptId() {
        return receiptId;
    }

    public void setReceiptId(String receiptId) {
        this.receiptId = receiptId;
    }

    public List<ReceiptItemDTO> getItems() {
        return items;
    }

    public void setItems(List<ReceiptItemDTO> items) {
        this.items = items;
    }

    public float getTotalSum() {
        return totalSum;
    }

    public void setTotalSum(float totalSum) {
        this.totalSum = totalSum;
    }

    public List<CategoryDTO> getCategories() {
        return categories;
    }

    public void setCategories(List<CategoryDTO> categories) {
        this.categories = categories;
    }

    public Map<String, Object> getDebugData() {
        return debugData;
    }

    public void setDebugData(Map<String, Object> debugData) {
        this.debugData = debugData;
    }
}